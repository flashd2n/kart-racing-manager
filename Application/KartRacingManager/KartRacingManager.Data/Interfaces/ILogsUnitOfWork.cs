using KartRacingManager.Data.Repositories.LogsDbRepositories;

namespace KartRacingManager.Data.Interfaces
{
    public interface ILogsUnitOfWork
    {
        LogsRepository LogsRepo { get; }

        LogTypesRepository LogTypesRepo { get; }

    }
}
