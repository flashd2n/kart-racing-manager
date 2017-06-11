using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Imports;
using KartRacingManager.Interfaces.Providers;

namespace KartRacingManager.Commands.Commands
{
    public class ImportTrackCommand : ICommand
    {

        private readonly IWriter writer;
        private readonly IMainUnitOfWork mainUnitOfWork;
        private readonly IXmlImporter xmlImporter;

        public ImportTrackCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IXmlImporter xmlImporter)
        {
            Guard.WhenArgument(mainUnitOfWork, "mainUnitOfWork").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(xmlImporter, "xmlImporter").IsNull().Throw();

            this.mainUnitOfWork = mainUnitOfWork;
            this.writer = writer;
            this.xmlImporter = xmlImporter;
        }

        public void Execute(params string[] commandParameters)
        {
            if (commandParameters.Length < 1)
            {
                this.writer.Write("Import path is required.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            this.xmlImporter.Path = commandParameters[0];

            var importedTrack = this.xmlImporter.Execute();

            string trackName = importedTrack["name"];
            double trackLength;
            try
            {
                trackLength = double.Parse(importedTrack["length"]);
            }
            catch (FormatException)
            {
                this.writer.Write("Track length should be a number.");
                this.writer.Write(Environment.NewLine);
                return;
            }

            string addressLocation = importedTrack["addressLocation"];
            string cityName = importedTrack["city"];
            string countryName = importedTrack["country"];

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
