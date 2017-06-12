using KartRacingManager.Data;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.KartDbRepositories;
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
    public class KartsUnitOfWorkTests
    {
        [Test]
        public void ConstuctorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange

            IKartDbContext contextStub = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new KartsUnitOfWork(contextStub));
        }

        [Test]
        public void ConstuctorShouldNotThrow_WhenValidContextIsPassed()
        {
            // Arrange

            var contextStub = new Mock<IKartDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new KartsUnitOfWork(contextStub.Object));
        }

        [Test]
        public void KartsRepoShouldReturnNewKartsRepository()
        {
            // Arrange

            var contextStub = new Mock<IKartDbContext>();

            var sut = new KartsUnitOfWork(contextStub.Object);

            // Act

            var result = sut.KartsRepo;

            // Assert

            Assert.IsInstanceOf<KartsRepository>(result);
        }

        [Test]
        public void TransmissionTypesRepoShouldReturnNewTransmissionTypesRepository()
        {
            // Arrange

            var contextStub = new Mock<IKartDbContext>();

            var sut = new KartsUnitOfWork(contextStub.Object);

            // Act

            var result = sut.TransmissionTypesRepo;

            // Assert

            Assert.IsInstanceOf<TransmissionTypesRepository>(result);
        }

        [Test]
        public void DisposeShouldCallContextDisposeMethod()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new KartsUnitOfWork(contextMock.Object);

            // Act

            sut.Dispose();

            // Assert

            contextMock.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void SaveShouldCallContextSaveChangesMethod()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new KartsUnitOfWork(contextMock.Object);

            // Act

            sut.Save();

            // Assert

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

    }
}
