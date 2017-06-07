using System.Data.Entity;
using KarRacingManager.Models;

namespace KartRacingManager.Interfaces.Data
{
    public interface IMainDbContext
    {
         DbSet<Racer> Racers { get; set; }
        
         DbSet<Address> Addresses { get; set; }
        
         DbSet<City> Cities { get; set; }
        
         DbSet<Country> Countries { get; set; }
        
         DbSet<Lap> Laps { get; set; }
        
         DbSet<Race> Races { get; set; }
        
         DbSet<Track> Tracks { get; set; }
    }
}
