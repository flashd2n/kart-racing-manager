using System;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class SetRacerWeightCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;

        public SetRacerWeightCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
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
                this.writer.Write("Racer id must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            double weight = double.NaN;
            try
            {
                weight = double.Parse(commandParameters[1]);
            }
            catch (FormatException)
            {
                this.writer.Write("Weight must be a number.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (weight < 0)
            {
                this.writer.Write("Weight must be a positive number.");
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

            var racerInfo = racer.DetailedInformation;

            if (racerInfo == null)
            {
                racerInfo = new DetailedRacersInformation();
                racer.DetailedInformation = racerInfo;
            }

            racerInfo.Weight = weight;

            this.mainUnitOfWork.Save();
        }
    }
}
