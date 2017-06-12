using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class LapsRepository
    {
        private IMainDbContext context;

        public LapsRepository(IMainDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public IQueryable<Lap> All
        {
            get { return this.context.Laps; }
        }

        public void Add(Lap entity)
        {
            this.context.Laps.Add(entity);
        }

        public Lap FindById(object id)
        {
            return this.context.Laps.Find(id);
        }

        public void Remove(Lap entity)
        {
            this.context.Laps.Remove(entity);
        }

        public void Update(Lap entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Lap entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
