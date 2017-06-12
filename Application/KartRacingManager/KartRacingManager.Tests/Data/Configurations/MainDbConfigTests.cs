using KartRacingManager.Data.Migrations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Tests.Data.Configurations
{
    [TestFixture]
    public class MainDbConfigTests
    {
        [Test]
        public void AutomaticMigrationsEnabledShouldBeFalse()
        {
            // Arrange

            var config = new MainDbConfig();

            var sut = new MainDbConfigContainer(config);

            // Act & Assert

            Assert.IsFalse(sut.Config.AutomaticMigrationsEnabled);

        }

        [Test]
        public void AutomaticMigrationDataLossAllowedShouldBeFalse()
        {
            // Arrange

            var config = new MainDbConfig();

            var sut = new MainDbConfigContainer(config);

            // Act & Assert

            Assert.IsFalse(sut.Config.AutomaticMigrationDataLossAllowed);
        }
    }
}
