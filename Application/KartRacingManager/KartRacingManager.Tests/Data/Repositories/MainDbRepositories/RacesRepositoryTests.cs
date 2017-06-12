using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Tests.Data.Repositories.MainDbRepositories
{
    [TestFixture]
    public class RacesRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new RacesRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new RacesRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyRacesOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacesRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Races, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnRacesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacesRepository(contextMock.Object);

            contextMock.Setup(x => x.Races.Add(It.IsAny<Race>()));

            var race = new Race();

            // Act

            sut.Add(race);

            // Assert

            contextMock.Verify(x => x.Races.Add(race), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnRacesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacesRepository(contextMock.Object);

            contextMock.Setup(x => x.Races.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Races.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnRacesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new RacesRepository(contextMock.Object);

            contextMock.Setup(x => x.Races.Remove(It.IsAny<Race>()));

            var race = new Race();

            // Act

            sut.Remove(race);

            // Assert

            contextMock.Verify(x => x.Races.Remove(race), Times.Once);

        }
    }
}
