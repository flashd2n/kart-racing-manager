using KarRacingManager.Models.PostgreSqlModels;
using System.Data.Entity;

namespace KartRacingManager.Data
{
    public class KartDbContext : DbContext
    {
        public KartDbContext() :base("PgsqlDb")
        {

        }

        public DbSet<Kart> Karts { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
