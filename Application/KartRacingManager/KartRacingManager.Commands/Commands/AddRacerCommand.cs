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

        public AddRacerCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 6)
            {
                throw new ArgumentException("Incorrect number of parameters.");
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
                throw new ArgumentException("Incorrect date format");
            }

            string addressLocation = commandParameters[3];
            string cityName = commandParameters[4];
            string countryName = commandParameters[5];

            var country = this.mainUnitOfWork.CountriesRepo.All
                .FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());

            if (country == null)
            {
                country = new Country();
                country.Name = countryName;
            }

            var city = this.mainUnitOfWork.CitiesRepo.All
                .FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());

            if (city == null)
            {
                city = new City();
                city.Name = cityName;
                city.Country = country;
            }

            var address = this.mainUnitOfWork.AddressesRepo.All
                .FirstOrDefault(a => a.Location.ToLower() == addressLocation.ToLower());

            if (address == null)
            {
                address = new Address();
                address.Location = addressLocation;
                address.City = city;
            }

            var racer = new Racer
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Address = address
            };

            this.mainUnitOfWork.RacersRepo.Add(racer);
            this.mainUnitOfWork.Save();
        }
    }
}
