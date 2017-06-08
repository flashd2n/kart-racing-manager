using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace KartRacingManager.Data.Interfaces
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
