using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.KartDbRepositories;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Data.Repositories.KartDbRepositories
{
    [TestFixture]
    public class KartsRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IKartDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new KartsRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IKartDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new KartsRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyKartsOnce()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new KartsRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Karts, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnKartsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new KartsRepository(contextMock.Object);

            contextMock.Setup(x => x.Karts.Add(It.IsAny<Kart>()));

            var kart = new Kart();

            // Act

            sut.Add(kart);

            // Assert

            contextMock.Verify(x => x.Karts.Add(kart), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnKartsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new KartsRepository(contextMock.Object);

            contextMock.Setup(x => x.Karts.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Karts.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnKartsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new KartsRepository(contextMock.Object);

            contextMock.Setup(x => x.Karts.Remove(It.IsAny<Kart>()));

            var kart = new Kart();

            // Act

            sut.Remove(kart);

            // Assert

            contextMock.Verify(x => x.Karts.Remove(kart), Times.Once);

        }
    }
}
