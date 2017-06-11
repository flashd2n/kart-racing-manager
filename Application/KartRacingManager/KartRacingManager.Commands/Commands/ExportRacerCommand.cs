using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Exports;
using KartRacingManager.Interfaces.Providers;
using System;
using System.Linq;

namespace KartRacingManager.Commands.Commands
{
    public class ExportRacerCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;
        private readonly IExporter exporter;

        public ExportRacerCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IExporter exporter)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            //Guard.WhenArgument(exporter, "exporter").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length <= 0)
            {
                this.writer.Write("ExportRacer racer_id");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 1)
            {
                this.writer.Write("Racer id is not provided.");
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
                this.writer.Write("Racer id must be integer.");
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

            // basic info
            this.writer.Write($"First name: {racer.FirstName}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"Last name: {racer.LastName}");
            this.writer.Write(Environment.NewLine);
            this.writer.Write($"Date of birth: {racer.DateOfBirth.Year}-" +
                              $"{racer.DateOfBirth.Month}-" +
                              $"{racer.DateOfBirth.Day}");
            this.writer.Write(Environment.NewLine);
            // address
            this.writer.Write($"Full address: {racer.Address.Location}, {racer.Address.City.Name}, {racer.Address.City.Country.Name}");
            this.writer.Write(Environment.NewLine);
            // detailed info
            string heightAsString = racer.DetailedInformation == null || racer.DetailedInformation.Height == null
                ? "N/A"
                : racer.DetailedInformation.Height.ToString();
            this.writer.Write($"Height: {heightAsString}");
            this.writer.Write(Environment.NewLine);
            string weightAsString = racer.DetailedInformation == null || racer.DetailedInformation.Weight == null
                ? "N/A"
                : racer.DetailedInformation.Weight.ToString();
            this.writer.Write($"Weight: {weightAsString}");
            this.writer.Write(Environment.NewLine);
            // races
            int numberOfCompletedRaces = this.mainUnitOfWork.RacesRepo.All
                .Count(race => race.RaceStatus == RaceStatus.Completed && race.Racers.Any(r => r.Id == racerId));
            this.writer.Write($"Number of races: {numberOfCompletedRaces}");
            this.writer.Write(Environment.NewLine);
            // number of laps
            int numberOfLaps = this.mainUnitOfWork.LapsRepo.All.Count(lap => lap.RacerId == racerId);
            this.writer.Write($"Total number of laps: {numberOfLaps}");
            this.writer.Write(Environment.NewLine);
        }
    }
}
