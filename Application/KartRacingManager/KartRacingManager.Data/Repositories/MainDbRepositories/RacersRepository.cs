using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class RacersRepository
    {
        private IMainDbContext context;

        public RacersRepository(IMainDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Racer> All
        {
            get { return this.context.Racers; }
        }

        public void Add(Racer entity)
        {
            this.context.Racers.Add(entity);
        }

        public Racer FindById(object id)
        {
            return this.context.Racers.Find(id);
        }

        public void Remove(Racer entity)
        {
            this.context.Racers.Remove(entity);
        }

        public void Update(Racer entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Racer entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
