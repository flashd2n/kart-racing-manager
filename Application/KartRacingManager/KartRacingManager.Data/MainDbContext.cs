using KarRacingManager.Models;
using KartRacingManager.Interfaces.Data;
using System.Data.Entity;

namespace KartRacingManager.Data
{
    public class MainDbContext : DbContext, IMainDbContext, IDbContext
    {
        public MainDbContext() : base("MainKartDb")
        {

        }

        public IDbSet<Racer> Racers { get; set; }

        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Lap> Laps { get; set; }

        public IDbSet<Race> Races { get; set; }

        public IDbSet<Track> Tracks { get; set; }

        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.OnRacerBuilding(modelBuilder);

            this.OnTrackBuilding(modelBuilder);

            this.OnAddressBuilding(modelBuilder);

            this.OnCityBuilding(modelBuilder);

            this.OnCountryBuilding(modelBuilder);

            this.OnRaceBuilding(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void OnRaceBuilding(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Race>()
                .Property(race => race.EndTime)
                .IsOptional();
            modelBuilder.Entity<Race>()
                .Property(race => race.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void OnCountryBuilding(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(country => country.Name)
                .HasMaxLength(60)
                .IsRequired();
        }

        private void OnCityBuilding(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(city => city.Name)
                .HasMaxLength(60)
                .IsRequired();
        }

        private void OnAddressBuilding(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(address => address.Location)
                .HasMaxLength(100)
                .IsRequired();
        }

        private void OnTrackBuilding(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>()
                .HasRequired(t => t.Address)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Track>()
                .Property(track => track.Name)
                .HasMaxLength(60)
                .IsRequired();
        }

        private void OnRacerBuilding(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Racer>()
                .HasOptional(s => s.DetailedInformation)
                .WithRequired(ad => ad.Racer);
        }
    }
}
