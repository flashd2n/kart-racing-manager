using KartRacingManager.Data.Repositories.KartDbRepositories;

namespace KartRacingManager.Data.Interfaces
{
    public interface IKartsUnitOfWork
    {
        KartsRepository RacersRepo { get; }

        TransmissionTypesRepository TransmissionTypesRepo { get; }

    }
}
