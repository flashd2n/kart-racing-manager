using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories
{
    public class AddressesRepository
    {
        private IMainDbContext context;

        public AddressesRepository(IMainDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Address> All
        {
            get { return this.context.Addresses; }
        }

        public void Add(Address entity)
        {
            this.context.Addresses.Add(entity);
        }

        public Address FindById(object id)
        {
            return this.context.Addresses.Find(id);
        }

        public void Remove(Address entity)
        {
            this.context.Addresses.Remove(entity);
        }

        public void Update(Address entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Address entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
