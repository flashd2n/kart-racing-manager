using Bytes2you.Validation;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.KartDbRepositories;

namespace KartRacingManager.Data
{
    public class KartsUnitOfWork : IKartsUnitOfWork, IUnitOfWork
    {
        private IKartDbContext context;

        private KartsRepository kartsRepo;
        private TransmissionTypesRepository transmissionTypesRepo;

        public KartsUnitOfWork(IKartDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public KartsRepository KartsRepo
        {
            get
            {
                if (this.kartsRepo == null)
                {
                    this.kartsRepo = new KartsRepository(this.context);
                }
                return this.kartsRepo;
            }
        }

        public TransmissionTypesRepository TransmissionTypesRepo
        {
            get
            {
                if (this.transmissionTypesRepo == null)
                {
                    this.transmissionTypesRepo = new TransmissionTypesRepository(this.context);
                }
                return this.transmissionTypesRepo;
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
