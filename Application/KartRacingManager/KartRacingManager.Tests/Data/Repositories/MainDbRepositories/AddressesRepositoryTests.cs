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
    class AddressesRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddressesRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new AddressesRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyAddressesOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new AddressesRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Addresses, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnAddressesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new AddressesRepository(contextMock.Object);

            contextMock.Setup(x => x.Addresses.Add(It.IsAny<Address>()));

            var address = new Address();

            // Act

            sut.Add(address);

            // Assert

            contextMock.Verify(x => x.Addresses.Add(address), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnAddressesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new AddressesRepository(contextMock.Object);

            contextMock.Setup(x => x.Addresses.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Addresses.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnAddressesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new AddressesRepository(contextMock.Object);

            contextMock.Setup(x => x.Addresses.Remove(It.IsAny<Address>()));

            var address = new Address();

            // Act

            sut.Remove(address);

            // Assert

            contextMock.Verify(x => x.Addresses.Remove(address), Times.Once);

        }
    }
}
