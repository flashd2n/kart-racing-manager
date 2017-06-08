using System.Data.Entity;
using KarRacingManager.Models;

namespace KartRacingManager.Data.Interfaces
{
    public interface IMainDbContext : IDbContext
    {
         IDbSet<Racer> Racers { get; set; }
        
         IDbSet<Address> Addresses { get; set; }
        
         IDbSet<City> Cities { get; set; }
        
         IDbSet<Country> Countries { get; set; }
        
         IDbSet<Lap> Laps { get; set; }
        
         IDbSet<Race> Races { get; set; }
        
         IDbSet<Track> Tracks { get; set; }
    }
}
