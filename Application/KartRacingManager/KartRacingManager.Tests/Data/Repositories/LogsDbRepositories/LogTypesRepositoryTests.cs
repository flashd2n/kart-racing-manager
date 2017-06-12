using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.LogsDbRepositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Tests.Data.Repositories.LogsDbRepositories
{
    [TestFixture]
    public class LogTypesRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            ILogsDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new LogTypesRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<ILogsDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new LogTypesRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyLogTypesOnce()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogTypesRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.LogTypes, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnLogTypesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogTypesRepository(contextMock.Object);

            contextMock.Setup(x => x.LogTypes.Add(It.IsAny<LogType>()));

            var logType = new LogType();

            // Act

            sut.Add(logType);

            // Assert

            contextMock.Verify(x => x.LogTypes.Add(logType), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnLogTypesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogTypesRepository(contextMock.Object);

            contextMock.Setup(x => x.LogTypes.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.LogTypes.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnLogTypesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogTypesRepository(contextMock.Object);

            contextMock.Setup(x => x.LogTypes.Remove(It.IsAny<LogType>()));

            var logType = new LogType();

            // Act

            sut.Remove(logType);

            // Assert

            contextMock.Verify(x => x.LogTypes.Remove(logType), Times.Once);

        }
    }
}
