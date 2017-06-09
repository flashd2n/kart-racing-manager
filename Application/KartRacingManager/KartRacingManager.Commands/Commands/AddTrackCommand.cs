using System;
using System.Linq;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class AddTrackCommand : ICommand
    {
        
        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;

        public AddTrackCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 5)
            {
                this.writer.Write("Incorrect number of parameters.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string trackName = commandParameters[0];
            double trackLength;
            try
            {
                trackLength = double.Parse(commandParameters[1]);
            }
            catch (FormatException)
            {
                this.writer.Write("Track length should be a number.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string addressLocation = commandParameters[2];
            string cityName = commandParameters[3];
            string countryName = commandParameters[4];

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

            var track = new Track
            {
                Name = trackName,
                Length = trackLength,
                Address = address
            };

            this.mainUnitOfWork.TracksRepo.Add(track);
            this.mainUnitOfWork.Save();
        }
    }
}

