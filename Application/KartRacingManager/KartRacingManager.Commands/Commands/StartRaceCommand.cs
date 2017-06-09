using System;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class StartRaceCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;

        public StartRaceCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 1)
            {
                this.writer.Write("Race id is not provided.");
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

            if (race.RaceStatus != RaceStatus.Pending)
            {
                this.writer.Write($"Race status is {race.RaceStatus}. " +
                                  $"Race status must be {RaceStatus.Pending} for the race to be started.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            race.RaceStatus = RaceStatus.InProgress;

            this.mainUnitOfWork.Save();
        }
    }
}
