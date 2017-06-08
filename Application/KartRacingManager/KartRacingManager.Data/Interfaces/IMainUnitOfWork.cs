using KartRacingManager.Data.Repositories;

namespace KartRacingManager.Data.Interfaces
{
    public interface IMainUnitOfWork : IUnitOfWork
    {
        RacersRepository RacersRepo { get; }

        AddressesRepository AddressesRepo { get; }

        CitiesRepository CitiesRepo { get; }

        CountriesRepository CountriesRepo { get; }

        LapsRepository LapsRepo { get; }

        RacesRepository RacesRepo { get; }

        TracksRepository TracksRepo { get; }

    }
}
