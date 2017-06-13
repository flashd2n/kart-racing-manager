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
        private readonly IModelFactory modelFactory;

        public AddTrackCommand(IMainUnitOfWork mainUnitOfWork, IWriter writer, IModelFactory modelFactory)
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
                this.writer.Write("AddTrack track_name track_length address_location city country");
                this.writer.Write(Environment.NewLine);
                return;
            }

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

            var track = this.modelFactory.CreateTrack();

            track.Name = trackName;
            track.Length = trackLength;
            track.Address = address;
            

            this.mainUnitOfWork.TracksRepo.Add(track);
            this.mainUnitOfWork.Save();
        }
    }
}

