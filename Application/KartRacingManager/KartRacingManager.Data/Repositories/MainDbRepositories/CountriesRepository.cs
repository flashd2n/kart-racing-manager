using Bytes2you.Validation;
using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class CountriesRepository
    {
        private IMainDbContext context;

        public CountriesRepository(IMainDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public IQueryable<Country> All
        {
            get { return this.context.Countries; }
        }

        public void Add(Country entity)
        {
            this.context.Countries.Add(entity);
        }

        public Country FindById(object id)
        {
            return this.context.Countries.Find(id);
        }

        public void Remove(Country entity)
        {
            this.context.Countries.Remove(entity);
        }

        public void Update(Country entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Country entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
