using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace KartRacingManager.Data.Repositories.KartDbRepositories
{
    public class TransmissionTypesRepository
    {
        private IKartDbContext context;

        public TransmissionTypesRepository(IKartDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TransmissionType> All
        {
            get { return this.context.TransmissionTypes; }
        }

        public void Add(TransmissionType entity)
        {
            this.context.TransmissionTypes.Add(entity);
        }

        public TransmissionType FindById(object id)
        {
            return this.context.TransmissionTypes.Find(id);
        }

        public void Remove(TransmissionType entity)
        {
            this.context.TransmissionTypes.Remove(entity);
        }

        public void Update(TransmissionType entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(TransmissionType entity, EntityState state)
        {
            var dbEntry = this.context.Entry(entity);
            dbEntry.State = state;
        }
    }
}
