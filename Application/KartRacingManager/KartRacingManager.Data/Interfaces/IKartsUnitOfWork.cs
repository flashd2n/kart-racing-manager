using KartRacingManager.Data.Repositories.KartDbRepositories;

namespace KartRacingManager.Data.Interfaces
{
    public interface IKartsUnitOfWork : IUnitOfWork
    {
        KartsRepository RacersRepo { get; }

        TransmissionTypesRepository TransmissionTypesRepo { get; }

    }
}
