using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Data.Repositories.MainDbRepositories
{
    [TestFixture]
    public class RacersRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new RacersRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new RacersRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyRacersOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacersRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Racers, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnRacersPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacersRepository(contextMock.Object);

            contextMock.Setup(x => x.Racers.Add(It.IsAny<Racer>()));

            var racer = new Racer();

            // Act

            sut.Add(racer);

            // Assert

            contextMock.Verify(x => x.Racers.Add(racer), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnRacersPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacersRepository(contextMock.Object);

            contextMock.Setup(x => x.Racers.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Racers.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnRacersPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacersRepository(contextMock.Object);

            contextMock.Setup(x => x.Racers.Remove(It.IsAny<Racer>()));

            var racer = new Racer();

            // Act

            sut.Remove(racer);

            // Assert

            contextMock.Verify(x => x.Racers.Remove(racer), Times.Once);

        }
    }
}
