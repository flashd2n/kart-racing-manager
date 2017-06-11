using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Exports;
using KartRacingManager.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KartRacingManager.Commands.Commands
{
    public class ExportRaceCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;
        private readonly IExporter exporter;

        public ExportRaceCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IExporter exporter)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(exporter, "exporter").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
            this.exporter = exporter;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length <= 0)
            {
                this.writer.Write("ExportRace race_id");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 1)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int raceId = 0;
            try
            {
                raceId = int.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("Race id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var race = this.mainUnitOfWork.RacesRepo.FindById(raceId);
            if (race == null)
            {
                this.writer.Write($"No race with id {raceId}.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var raceInfo = RaceInfoToDictionary(race, raceId);

            this.exporter.ExportRaceInfo(raceInfo);
        }

        private Dictionary<string, string> RaceInfoToDictionary(Race race, int raceId)
        {

            var racers = new List<string>();
            foreach (var racer in race.Racers)
            {   
                racers.Add($"{racer.Id} - {racer.FirstName} {racer.LastName}");

            }
           
            var raceInfo = new Dictionary<string, string>();
            raceInfo.Add("Race name", race.Name);
            raceInfo.Add("Status", $"{race.RaceStatus}");
            raceInfo.Add("Start time", $"{race.StartTime}");
            raceInfo.Add("End time", $"{race.EndTime}");
            raceInfo.Add("Laps", $"{race.LapCount}");
            raceInfo.Add("Track", $"{race.Track.Name}");
            raceInfo.Add("Racers", string.Join("\n  ", racers));
             
            return RenameEmptyString(raceInfo);
        }

        private Dictionary<string, string> RenameEmptyString(Dictionary<string, string> information)
        {
            foreach (var key in information.Keys.ToList())
            {
                if (String.IsNullOrEmpty(information[key]) || information[key] == "")
                {
                    information[key] = "N/A";
                }
            }

            return information;
        }
    }
}
