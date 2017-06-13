using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;
using KarRacingManager.Models;

namespace KartRacingManager.Commands.Commands
{
    public class AddKartCommand : ICommand
    {
        private readonly IKartsUnitOfWork kartsUnitOfWork;
        private readonly IWriter writer;
        private readonly IModelFactory modelFactory;

        public AddKartCommand(IKartsUnitOfWork kartsUnitOfWork, IWriter writer, IModelFactory modelFactory)
        {
            Guard.WhenArgument(kartsUnitOfWork, "kartsUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(modelFactory, "modelFactory").IsNull().Throw();

            this.kartsUnitOfWork = kartsUnitOfWork;
            this.writer = writer;
            this.modelFactory = modelFactory;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length <= 0)
            {
                this.writer.Write("AddKart horsepower top_speed_in_km transmission_type");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 3)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            int horsepower = 0;
            try
            {
                horsepower = int.Parse(commandParameters[0]);
            }
            catch (FormatException)
            {
                this.writer.Write("Horsepower must be integer.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            double topSpeedInKph = double.NaN;
            try
            {
                topSpeedInKph = double.Parse(commandParameters[1]);
            }
            catch (FormatException)
            {
                this.writer.Write("Top speed must be a number.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string transmissionTypeAsString = commandParameters[2].ToLower();

            TransmissionType transmissionType = this.kartsUnitOfWork.TransmissionTypesRepo.All.FirstOrDefault(tt => tt.Name == transmissionTypeAsString);

            if (transmissionType == null)
            {
                transmissionType = this.modelFactory.CreateTransmissionType();
                
                transmissionType.Name = transmissionTypeAsString;
                
            }


            var kart = this.modelFactory.CreateKart();
            kart.HorsePower = horsepower;
            kart.TopSpeedKph = topSpeedInKph;
            kart.TransmissionType = transmissionType;

            this.kartsUnitOfWork.KartsRepo.Add(kart);

            this.kartsUnitOfWork.Save();
        }
    }
}
