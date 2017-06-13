using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Logger;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Logger
{
    [TestFixture]
    public class SqliteLoggerTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidLogsUnitOfWorkIsPassed()
        {
            // Arrage

            ILogsUnitOfWork logsUnitOfwork = null;
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new SqliteLogger(logsUnitOfwork, dateTimeProvider.Object, modelFactory.Object));

        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidDateTimeProviderIsPassed()
        {
            // Arrage

            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            IDateTimeProvider dateTimeProvider = null;
            var modelFactory = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider, modelFactory.Object));

        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidModelsFactoryIsPassed()
        {
            // Arrage

            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            IModelFactory modelFactory = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory));

        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidParametersArePassed()
        {
            // Arrage

            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            // Act & Assert

            Assert.DoesNotThrow(() => new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object));
        }

        [Test]
        public void LogShouldCreateNewLogType_WhenLogTypeIsNotFoundInDatabase()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

        }


    }
}
