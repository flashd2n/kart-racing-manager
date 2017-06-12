using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Data.Repositories.MainDbRepositories
{
    [TestFixture]
    public class CitiesRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new CitiesRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new CitiesRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyCitiesOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CitiesRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Cities, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnCitiesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CitiesRepository(contextMock.Object);

            contextMock.Setup(x => x.Cities.Add(It.IsAny<City>()));

            var city = new City();

            // Act

            sut.Add(city);

            // Assert

            contextMock.Verify(x => x.Cities.Add(city), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnCitiesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CitiesRepository(contextMock.Object);

            contextMock.Setup(x => x.Cities.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Cities.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnCitiessPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CitiesRepository(contextMock.Object);

            contextMock.Setup(x => x.Cities.Remove(It.IsAny<City>()));

            var city = new City();

            // Act

            sut.Remove(city);

            // Assert

            contextMock.Verify(x => x.Cities.Remove(city), Times.Once);

        }
    }
}
