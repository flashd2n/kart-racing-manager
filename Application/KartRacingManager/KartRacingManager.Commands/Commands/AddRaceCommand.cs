using System;
using System.Linq;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class AddRaceCommand : ICommand
    {

        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;

        public AddRaceCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 4)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string raceName = commandParameters[0];
            int trackId;
            try
            {
                trackId = int.Parse(commandParameters[1]);
            }
            catch (FormatException)
            {
                this.writer.Write("Track id must be an integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (!this.mainUnitOfWork.TracksRepo.All.Any(t => t.Id == trackId))
            {
                this.writer.Write($"Track with id {trackId} does not exist.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            DateTime raceStartTime;
            try
            {
                raceStartTime = DateTime.Parse(commandParameters[2]);
            }
            catch (FormatException)
            {
                this.writer.Write("Incorrect start time.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int lapCount;
            try
            {
                lapCount = int.Parse(commandParameters[3]);
            }
            catch (FormatException)
            {
                this.writer.Write("Lap count must be an integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (lapCount <= 0)
            {
                this.writer.Write("Lap count must be positive number.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var race = new Race
            {
                Name = raceName,
                TrackId = trackId,
                StartTime = raceStartTime,
                LapCount = lapCount,
                RaceStatus = RaceStatus.Pending
            };

            this.mainUnitOfWork.RacesRepo.Add(race);
            this.mainUnitOfWork.Save();
        }
    }
}
