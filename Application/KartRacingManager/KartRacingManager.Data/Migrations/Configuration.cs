using KarRacingManager.Models;
using System;
using System.Data.Entity.Migrations;

namespace KartRacingManager.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MainDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MainDbContext context)
        {

            for (int i = 0; i < 10; i++)
            {
                var lap = new Lap();

                lap.StartTime = DateTime.Now.AddDays(i);

                lap.FinishTime = DateTime.Now.AddDays(i + 1);

                lap.Racer = this.CreateRacer(context, i);

                lap.Track = this.CreateTrack(context, i);

                
                context.Laps.AddOrUpdate(lap);
            }
            
        }

        private Track CreateTrack(MainDbContext context, int i)
        {
            var country = new Country();
            country.Name = "track country " + i;

            var city = new City();
            city.Name = "track city " + i;
            city.Country = country;

            var address = new Address();
            address.City = city;
            address.Location = "track address " + i;

            var track = new Track();
            track.Length = 4.23;
            track.Name = "seed track " + i;
            track.Address = address;

            return track;
        }

        private Racer CreateRacer(MainDbContext context, int i)
        {
            var country = new Country();
            country.Name = "seed country " + i;

            var city = new City();
            city.Name = "seed city " + i;
            city.Country = country;

            var address = new Address();
            address.City = city;
            address.Location = "seed address " + i;

            var racer = new Racer();
            racer.DateOfBirth = DateTime.Now;
            racer.FirstName = "Seed First Name " + i;
            racer.LastName = "Seed Last Name " + i;
            racer.Address = address;

            return racer;
        }
    }
}
