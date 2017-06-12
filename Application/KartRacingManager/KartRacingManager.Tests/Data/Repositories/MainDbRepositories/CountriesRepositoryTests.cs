using KarRacingManager.Models;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Data.Repositories.MainDbRepositories
{
    [TestFixture]
    public class CountriesRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new CountriesRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new CountriesRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyCountriesOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CountriesRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Countries, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnCountriesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CountriesRepository(contextMock.Object);

            contextMock.Setup(x => x.Countries.Add(It.IsAny<Country>()));

            var country = new Country();

            // Act

            sut.Add(country);

            // Assert

            contextMock.Verify(x => x.Countries.Add(country), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnCountriesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CountriesRepository(contextMock.Object);

            contextMock.Setup(x => x.Countries.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Countries.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnCountriesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new CountriesRepository(contextMock.Object);

            contextMock.Setup(x => x.Countries.Remove(It.IsAny<Country>()));

            var country = new Country();

            // Act

            sut.Remove(country);

            // Assert

            contextMock.Verify(x => x.Countries.Remove(country), Times.Once);

        }
    }
}
