using System;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class SetRacerHeightCommand : ICommand
    {
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IWriter writer;

        public SetRacerHeightCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
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
                this.writer.Write("SetRacerHeight racer_id racer_height");
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

            double height = double.NaN;
            try
            {
                height = double.Parse(commandParameters[1]);
            }
            catch (FormatException)
            {
                this.writer.Write("Height must be a number.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (height < 0)
            {
                this.writer.Write("Height must be a positive number.");
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

            racerInfo.Height = height;

            this.mainUnitOfWork.Save();
        }
    }
}
