using System;
using System.Linq;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class AddRacerCommand : ICommand

    {
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IModelFactory modelFactory;

        public AddRacerCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IModelFactory modelFactory)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(modelFactory, "modelFactory").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
            this.modelFactory = modelFactory;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length <= 0)
            {
                this.writer.Write("AddRacer first_name last_name date_of_birth address_location city country");
                this.writer.Write(Environment.NewLine);
                return;
            }

            if (commandParameters.Length < 6)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string firstName = commandParameters[0];
            string lastName = commandParameters[1];
            DateTime dateOfBirth;
            try
            {
                dateOfBirth = DateTime.Parse(commandParameters[2]);
            }
            catch (FormatException)
            {
                this.writer.Write("Incorrect date format");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string addressLocation = commandParameters[3];
            string cityName = commandParameters[4];
            string countryName = commandParameters[5];

            var country = this.mainUnitOfWork.CountriesRepo.All
                .FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());

            if (country == null)
            {
                country = this.modelFactory.CreateCountry();
                country.Name = countryName;
            }

            var city = this.mainUnitOfWork.CitiesRepo.All
                .FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());

            if (city == null)
            {
                city = this.modelFactory.CreateCity();
                city.Name = cityName;
                city.Country = country;
            }

            var address = this.mainUnitOfWork.AddressesRepo.All
                .FirstOrDefault(a => a.Location.ToLower() == addressLocation.ToLower());

            if (address == null)
            {
                address = this.modelFactory.CreateAddress();
                address.Location = addressLocation;
                address.City = city;
            }

            var racer = this.modelFactory.CreateRacer();            
            racer.FirstName = firstName;
            racer.LastName = lastName;
            racer.DateOfBirth = dateOfBirth;
            racer.Address = address;
            

            this.mainUnitOfWork.RacersRepo.Add(racer);
            this.mainUnitOfWork.Save();
        }
    }
}
