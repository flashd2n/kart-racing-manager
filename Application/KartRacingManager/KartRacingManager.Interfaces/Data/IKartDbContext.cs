using KarRacingManager.Models.PostgreSqlModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace KartRacingManager.Interfaces.Data
{
    public interface IKartDbContext
    {
        IDbSet<Kart> Karts { get; set; }

        IDbSet<TransmissionType> TransmissionTypes { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
