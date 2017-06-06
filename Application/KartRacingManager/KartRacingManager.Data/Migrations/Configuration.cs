using KarRacingManager.Models;
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Text;

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
            var startDate = DateTime.Parse("2017/06/06 18:37:58");

            for (int i = 0; i < 10; i++)
            {
                var lap = new Lap();

                lap.StartTime = startDate.AddDays(i);

                lap.FinishTime = DateTime.Now.AddDays(i + 1);

                lap.Racer = this.CreateRacer(context, i);

                lap.Track = this.CreateTrack(context, i);

                context.Laps.AddOrUpdate(l => l.StartTime, lap);
            }

            //TODO pending race

            var race = new Race();
            race.Name = "Seed Pending Race";
            race.StartTime = DateTime.Now.AddDays(1);
            race.LapCount = 10;
            race.RaceStatus = RaceStatus.Pending;
            var raceStatusParam = "Pending";
            race.Track = this.CreateTrack(context, raceStatusParam);

            context.Races.AddOrUpdate(r => r.Name, race);

            //TODO completed race

            var raceCompleted = new Race();
            raceCompleted.Name = "Seed Completed Race";
            raceCompleted.StartTime = DateTime.Now.AddDays(1);
            raceCompleted.EndTime = DateTime.Now.AddDays(2);
            raceCompleted.LapCount = 5;
            raceCompleted.RaceStatus = RaceStatus.Completed;
            var raceCompletedParam = "Completed";
            raceCompleted.Track = this.CreateTrack(context, raceCompletedParam);

            context.Races.AddOrUpdate(r => r.Name, raceCompleted);

            //this.SaveChanges(context);
        }

        private Track CreateTrack(MainDbContext context, string raceStatusParam)
        {
            var country = new Country();
            country.Name = "race country " + raceStatusParam;

            var city = new City();
            city.Name = "race city " + raceStatusParam;
            city.Country = country;

            var address = new Address();
            address.City = city;
            address.Location = "race address " + raceStatusParam;

            var track = new Track();
            track.Length = 4.23;
            track.Name = "race track " + raceStatusParam;
            track.Address = address;

            return track;
        }

        //private void SaveChanges(MainDbContext context)
        //{
        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        foreach (var failure in ex.EntityValidationErrors)
        //        {
        //            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
        //            foreach (var error in failure.ValidationErrors)
        //            {
        //                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
        //                sb.AppendLine();
        //            }
        //        }

        //        throw new DbEntityValidationException(
        //            "Entity Validation Failed - errors follow:\n" +
        //            sb.ToString(), ex
        //        ); // Add the original exception as the innerException
        //    }
        //}

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
