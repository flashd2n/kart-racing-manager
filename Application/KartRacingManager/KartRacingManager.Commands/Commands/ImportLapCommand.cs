using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Imports;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class ImportLapCommand : ICommand
    {
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IJsonImporter jsonImporter;

        public ImportLapCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IJsonImporter jsonImporter)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(jsonImporter, "jsonImporter").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
            this.jsonImporter = jsonImporter;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 1)
            {
                this.writer.Write("Import path is required.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string importPath = commandParameters[0];

            this.jsonImporter.Path = importPath;

            var lapImport = this.jsonImporter.Execute();

            DateTime lapStartTime;
            try
            {
                lapStartTime = DateTime.Parse(lapImport["StartTime"]);
            }
            catch (FormatException)
            {
                this.writer.Write("Incorrect start time.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int lapTimeInMiliseconds = 0;
            DateTime lapEndTime;
            try
            {
                lapEndTime = DateTime.Parse(lapImport["FinishTime"]);
                lapTimeInMiliseconds = (int)(lapEndTime - lapStartTime).TotalMilliseconds;
            }
            catch (FormatException)
            {
                this.writer.Write("Incorrect lap end time.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int racerId = 0;
            try
            {
                racerId = int.Parse(lapImport["RacerId"]);
                if (this.mainUnitOfWork.RacersRepo.FindById(racerId) == null)
                {
                    this.writer.Write($"Racer with id  {racerId} does not exist.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }
            catch (FormatException)
            {
                this.writer.Write("Racer id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int trackId = 0;
            try
            {
                trackId = int.Parse(lapImport["TrackId"]);
                if (this.mainUnitOfWork.TracksRepo.FindById(trackId) == null)
                {
                    this.writer.Write($"Track with id  {trackId} does not exist.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }
            catch (FormatException)
            {
                this.writer.Write("Track id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int? raceId = null;
            if (lapImport.ContainsKey("RaceId"))
            {
                try
                {
                    raceId = int.Parse(lapImport["RaceId"]);
                }
                catch (FormatException)
                {
                    this.writer.Write("Race id must be integer.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }

            if (raceId != null)
            {
                var race = this.mainUnitOfWork.RacesRepo.FindById(raceId);
                if (race == null)
                {
                    this.writer.Write($"Race with id of {raceId} does not exist.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }

                if (race.RaceStatus != RaceStatus.InProgress)
                {
                    this.writer.Write($"Race with id of {raceId} is in state {race.RaceStatus}. " +
                                      $"Only races in state {RaceStatus.InProgress} can be added laps.");
                    this.writer.Write(Environment.NewLine);
                    return;
                }
            }

            var lap = new Lap
            {
                StartTime = lapStartTime,
                FinishTime = lapEndTime,
                LapTimeInMs = lapTimeInMiliseconds,
                RacerId = racerId,
                TrackId = trackId,
                RaceId = raceId
            };

            this.mainUnitOfWork.LapsRepo.Add(lap);

            this.mainUnitOfWork.Save();
        }
    }
}
