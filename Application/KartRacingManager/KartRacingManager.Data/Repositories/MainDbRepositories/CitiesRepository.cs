using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class CitiesRepository
    {
        private IMainDbContext context;

        public CitiesRepository(IMainDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public IQueryable<City> All
        {
            get { return this.context.Cities; }
        }

        public void Add(City entity)
        {
            this.context.Cities.Add(entity);
        }

        public City FindById(object id)
        {
            return this.context.Cities.Find(id);
        }

        public void Remove(City entity)
        {
            this.context.Cities.Remove(entity);
        }

        public void Update(City entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(City entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
