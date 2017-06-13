using KartRacingManager.Data;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.KartDbRepositories;
using KartRacingManager.Data.Repositories.LogsDbRepositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Tests.Data.UnitsOfWork
{
    [TestFixture]
    public class LogsUnitOfWorkTests
    {
        [Test]
        public void ConstuctorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange

            ILogsDbContext contextStub = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new LogsUnitOfWork(contextStub));
        }

        [Test]
        public void ConstuctorShouldNotThrow_WhenValidContextIsPassed()
        {
            // Arrange

            var contextStub = new Mock<ILogsDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new LogsUnitOfWork(contextStub.Object));
        }

        [Test]
        public void LogsRepoShouldReturnNewLogsRepository()
        {
            // Arrange

            var contextStub = new Mock<ILogsDbContext>();

            var sut = new LogsUnitOfWork(contextStub.Object);

            // Act

            var result = sut.LogsRepo;

            // Assert

            Assert.IsInstanceOf<LogsRepository>(result);
        }

        [Test]
        public void LogTypesRepoShouldReturnNewLogTypesRepository()
        {
            // Arrange

            var contextStub = new Mock<ILogsDbContext>();

            var sut = new LogsUnitOfWork(contextStub.Object);

            // Act

            var result = sut.LogTypesRepo;
            // Assert

            Assert.IsInstanceOf<LogTypesRepository>(result);
        }

        [Test]
        public void DisposeShouldCallContextDisposeMethod()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogsUnitOfWork(contextMock.Object);

            // Act

            sut.Dispose();

            // Assert

            contextMock.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void SaveShouldCallContextSaveChangesMethod()
        {
            // Arrange

            var contextMock = new Mock<ILogsDbContext>();

            var sut = new LogsUnitOfWork(contextMock.Object);

            // Act

            sut.Save();

            // Assert

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }


    }
}
