using KartRacingManager.Data.Repositories.KartDbRepositories;

namespace KartRacingManager.Data.Interfaces
{
    public interface IKartsUnitOfWork : IUnitOfWork
    {
        KartsRepository KartsRepo { get; }

        TransmissionTypesRepository TransmissionTypesRepo { get; }

    }
}
