using KartRacingManager.Data;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;
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
    public class MainUnitOfWorkTests
    {
        [Test]
        public void ConstuctorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange

            IMainDbContext contextStub = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new MainUnitOfWork(contextStub));
        }

        [Test]
        public void ConstuctorShouldNotThrow_WhenValidContextIsPassed()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new MainUnitOfWork(contextStub.Object));
        }

        [Test]
        public void RacersRepoShouldReturnNewRacersRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.RacersRepo;

            // Assert

            Assert.IsInstanceOf<RacersRepository>(result);
        }

        [Test]
        public void AddressesRepoShouldReturnNewAddressesRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.AddressesRepo;

            // Assert

            Assert.IsInstanceOf<AddressesRepository>(result);
        }

        [Test]
        public void CitiesRepoShouldReturnNewCitiesRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.CitiesRepo;

            // Assert

            Assert.IsInstanceOf<CitiesRepository>(result);
        }

        [Test]
        public void CountriesRepoShouldReturnNewCountriesRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.CountriesRepo;

            // Assert

            Assert.IsInstanceOf<CountriesRepository>(result);
        }

        [Test]
        public void LapsRepoShouldReturnNewLapsRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.LapsRepo;

            // Assert

            Assert.IsInstanceOf<LapsRepository>(result);
        }

        [Test]
        public void RacesRepoShouldReturnNewRacesRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.RacesRepo;

            // Assert

            Assert.IsInstanceOf<RacesRepository>(result);
        }

        [Test]
        public void TracksRepoShouldReturnNewTracksRepository()
        {
            // Arrange

            var contextStub = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextStub.Object);

            // Act

            var result = sut.TracksRepo;

            // Assert

            Assert.IsInstanceOf<TracksRepository>(result);
        }

        [Test]
        public void DisposeShouldCallContextDisposeMethod()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextMock.Object);

            // Act

            sut.Dispose();

            // Assert

            contextMock.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void SaveShouldCallContextSaveChangesMethod()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new MainUnitOfWork(contextMock.Object);

            // Act

            sut.Save();

            // Assert

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
