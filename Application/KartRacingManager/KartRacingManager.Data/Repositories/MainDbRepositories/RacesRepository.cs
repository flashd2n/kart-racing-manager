using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class RacesRepository
    {
        private IMainDbContext context;

        public RacesRepository(IMainDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Race> All
        {
            get { return this.context.Races; }
        }

        public void Add(Race entity)
        {
            this.context.Races.Add(entity);
        }

        public Race FindById(object id)
        {
            return this.context.Races.Find(id);
        }

        public void Remove(Race entity)
        {
            this.context.Races.Remove(entity);
        }

        public void Update(Race entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Race entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
