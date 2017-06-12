using Bytes2you.Validation;
using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories.KartDbRepositories
{
    public class KartsRepository
    {
        private IKartDbContext context;

        public KartsRepository(IKartDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public IQueryable<Kart> All
        {
            get { return this.context.Karts; }
        }

        public void Add(Kart entity)
        {
            this.context.Karts.Add(entity);
        }

        public Kart FindById(object id)
        {
            return this.context.Karts.Find(id);
        }

        public void Remove(Kart entity)
        {
            this.context.Karts.Remove(entity);
        }

        public void Update(Kart entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(Kart entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
