using KartRacingManager.Interfaces.Data;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IDbContext context;
        private readonly IDbSet<TEntity> set;

        public EntityFrameworkRepository(IDbContext context)
        {
            this.context = context;
            this.set = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public IQueryable<TEntity> All()
        {
            return this.set;
        }

        public TEntity FindById(object id)
        {
            return this.set.Find(id);
        }

        public void Remove(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public void Update(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private void ChangeState(TEntity entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
