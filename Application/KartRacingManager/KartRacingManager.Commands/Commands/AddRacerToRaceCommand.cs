using System;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class AddRacerToRaceCommand : ICommand
    {
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;

        public AddRacerToRaceCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length <= 0)
            {
                this.writer.Write("AddRacerToRace racer_id race_id");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 2)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int racerId = 0;
            try
            {
                racerId = int.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("User id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int raceId = 0;
            try
            {
                raceId = int.Parse(commandParameters[1]);
            }
            catch (FormatException)
            {
                this.writer.Write("Race id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            var racer = this.mainUnitOfWork.RacersRepo.FindById(racerId);
            if (racer == null)
            {
                this.writer.Write($"No racer with id {racerId}.");
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
                this.writer.Write($"Racers can only be added to races in state RaceStatus.Pending.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            race.Racers.Add(racer);

            this.mainUnitOfWork.Save();
        }
    }
}
