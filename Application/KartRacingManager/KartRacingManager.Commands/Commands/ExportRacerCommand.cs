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
    public class ExportRacerCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;
        private readonly IExporter exporter;

        public ExportRacerCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IExporter exporter)
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

            var racerInfo = RacerInfoToDictionary(racer, racerId);

            this.exporter.ExportRacerInfo(racerInfo);
        }


        private Dictionary<string, string> RacerInfoToDictionary(Racer racer, int racerId)
        {
            int numberOfCompletedRaces = this.mainUnitOfWork.RacesRepo.All
                .Count(race => race.RaceStatus == RaceStatus.Completed && race.Racers.Any(r => r.Id == racerId));

            int numberOfLaps = this.mainUnitOfWork.LapsRepo.All.Count(lap => lap.RacerId == racerId);

            var racerInfo = new Dictionary<string, string>();
            
            racerInfo.Add("First name", racer.FirstName);
            racerInfo.Add("Last name", racer.LastName);
            racerInfo.Add("Date of birth", $"{racer.DateOfBirth.Day}-{racer.DateOfBirth.Month}-{racer.DateOfBirth.Year}");
            racerInfo.Add("Address", $"{racer.Address.Location}, {racer.Address.City.Name}, {racer.Address.City.Country.Name}");
            racerInfo.Add("Height", $"{racer.DetailedInformation.Height}cm");
            racerInfo.Add("Weight", $"{racer.DetailedInformation.Weight}kg");
            racerInfo.Add("Number of races", $"{numberOfCompletedRaces}");
            racerInfo.Add("Number of laps", $"{numberOfLaps}");

            return racerInfo;
        }
    }
}
