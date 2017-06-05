using KarRacingManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base("MainKartDb")
        {

        }

        public DbSet<Racer> Racers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Lap> Laps { get; set; }

        public DbSet<Race> Races { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Racer>()
                .HasOptional(s => s.DetailedInformation)
                .WithRequired(ad => ad.Racer);


            modelBuilder.Entity<Track>()
                .HasRequired(t => t.Address)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
