using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace KartRacingManager.Tests.Data.Repositories.MainDbRepositories
{
    [TestFixture]
    public class LapsRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new LapsRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new LapsRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyLapsOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new LapsRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Laps, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnLapsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new LapsRepository(contextMock.Object);

            contextMock.Setup(x => x.Laps.Add(It.IsAny<Lap>()));

            var lap = new Lap();

            // Act

            sut.Add(lap);

            // Assert

            contextMock.Verify(x => x.Laps.Add(lap), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnLapsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new LapsRepository(contextMock.Object);

            contextMock.Setup(x => x.Laps.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Laps.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnLapsPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new LapsRepository(contextMock.Object);

            contextMock.Setup(x => x.Laps.Remove(It.IsAny<Lap>()));

            var lap = new Lap();

            // Act

            sut.Remove(lap);

            // Assert

            contextMock.Verify(x => x.Laps.Remove(lap), Times.Once);

        }
    }
}
