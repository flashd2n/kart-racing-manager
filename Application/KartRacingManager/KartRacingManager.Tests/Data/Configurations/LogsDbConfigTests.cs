using KartRacingManager.Data.SqliteMigrations;
using NUnit.Framework;

namespace KartRacingManager.Tests.Data.Configurations
{
    [TestFixture]
    public class LogsDbConfigTests
    {
        [Test]
        public void AutomaticMigrationsEnabledShouldBeFalse()
        {
            // Arrange

            var config = new LogsDbConfig();

            var sut = new LogsDbConfigContainer(config);

            // Act & Assert

            Assert.IsFalse(sut.Config.AutomaticMigrationsEnabled);

        }

        [Test]
        public void AutomaticMigrationDataLossAllowedShouldBeFalse()
        {
            // Arrange

            var config = new LogsDbConfig();

            var sut = new LogsDbConfigContainer(config);

            // Act & Assert

            Assert.IsFalse(sut.Config.AutomaticMigrationDataLossAllowed);
        }
    }
}
