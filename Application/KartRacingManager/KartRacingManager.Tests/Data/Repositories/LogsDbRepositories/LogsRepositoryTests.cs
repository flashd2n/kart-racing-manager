using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.LogsDbRepositories;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Data.Repositories.LogsDbRepositories
{
    [TestFixture]
    public class LogsRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            ILogsDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new LogsRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<ILogsDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new LogsRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyLogsOnce()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogsRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Logs, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnLogsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogsRepository(contextMock.Object);

            contextMock.Setup(x => x.Logs.Add(It.IsAny<Log>()));

            var log = new Log();

            // Act

            sut.Add(log);

            // Assert

            contextMock.Verify(x => x.Logs.Add(log), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnLogsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogsRepository(contextMock.Object);

            contextMock.Setup(x => x.Logs.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Logs.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnLogsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogsRepository(contextMock.Object);

            contextMock.Setup(x => x.Logs.Remove(It.IsAny<Log>()));

            var log = new Log();

            // Act

            sut.Remove(log);

            // Assert

            contextMock.Verify(x => x.Logs.Remove(log), Times.Once);

        }


    }
}
