using KarRacingManager.Models.PostgreSqlModels;
using System.Data.Entity;

namespace KartRacingManager.Data.Interfaces
{
    public interface IKartDbContext : IDbContext
    {
        IDbSet<Kart> Karts { get; set; }

        IDbSet<TransmissionType> TransmissionTypes { get; set; }
    }
}
