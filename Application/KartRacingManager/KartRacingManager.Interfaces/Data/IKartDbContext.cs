using KarRacingManager.Models.PostgreSqlModels;
using System.Data.Entity;

namespace KartRacingManager.Interfaces.Data
{
    public interface IKartDbContext : IDbContext
    {
        IDbSet<Kart> Karts { get; set; }

        IDbSet<TransmissionType> TransmissionTypes { get; set; }
    }
}
