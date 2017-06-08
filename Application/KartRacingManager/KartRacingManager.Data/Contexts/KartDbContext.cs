using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using System.Data.Entity;

namespace KartRacingManager.Data
{
    public class KartDbContext : DbContext, IKartDbContext, IDbContext
    {
        public KartDbContext() :base("PgsqlDb")
        {

        }

        public IDbSet<Kart> Karts { get; set; }

        public IDbSet<TransmissionType> TransmissionTypes { get; set; }

        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
