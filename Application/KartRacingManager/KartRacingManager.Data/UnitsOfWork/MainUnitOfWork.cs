using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;

namespace KartRacingManager.Data
{
    public class MainUnitOfWork : IMainUnitOfWork, IUnitOfWork
    {
        private IMainDbContext context;

        private RacersRepository racersRepo;
        private AddressesRepository addressesRepo;
        private CitiesRepository citiesRepo;
        private CountriesRepository countriesRepo;
        private LapsRepository lapsRepo;
        private RacesRepository racesRepo;
        private TracksRepository tracksRepo;

        public MainUnitOfWork(IMainDbContext context)
        {
            this.context = context;
        }

        public RacersRepository RacersRepo
        {
            get
            {
                if (this.racersRepo == null)
                {
                    this.racersRepo = new RacersRepository(this.context);
                }
                return this.racersRepo;
            }
        }

        public AddressesRepository AddressesRepo
        {
            get
            {
                if (this.addressesRepo == null)
                {
                    this.addressesRepo = new AddressesRepository(this.context);
                }
                return this.addressesRepo;
            }
        }

        public CitiesRepository CitiesRepo
        {
            get
            {
                if (this.citiesRepo == null)
                {
                    this.citiesRepo = new CitiesRepository(this.context);
                }
                return this.citiesRepo;
            }
        }

        public CountriesRepository CountriesRepo
        {
            get
            {
                if (this.countriesRepo == null)
                {
                    this.countriesRepo = new CountriesRepository(this.context);
                }
                return this.countriesRepo;
            }
        }

        public LapsRepository LapsRepo
        {
            get
            {
                if (this.lapsRepo == null)
                {
                    this.lapsRepo = new LapsRepository(this.context);
                }
                return this.lapsRepo;
            }
        }

        public RacesRepository RacesRepo
        {
            get
            {
                if (this.racesRepo == null)
                {
                    this.racesRepo = new RacesRepository(this.context);
                }
                return this.racesRepo;
            }
        }

        public TracksRepository TracksRepo
        {
            get
            {
                if (this.tracksRepo == null)
                {
                    this.tracksRepo = new TracksRepository(this.context);
                }
                return this.tracksRepo;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
