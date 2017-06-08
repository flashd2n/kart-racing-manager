using System.Data.Entity;
using KarRacingManager.Models;
using System.Data.Entity.Infrastructure;

namespace KartRacingManager.Interfaces.Data
{
    public interface IMainDbContext
    {
         IDbSet<Racer> Racers { get; set; }
        
         IDbSet<Address> Addresses { get; set; }
        
         IDbSet<City> Cities { get; set; }
        
         IDbSet<Country> Countries { get; set; }
        
         IDbSet<Lap> Laps { get; set; }
        
         IDbSet<Race> Races { get; set; }
        
         IDbSet<Track> Tracks { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
