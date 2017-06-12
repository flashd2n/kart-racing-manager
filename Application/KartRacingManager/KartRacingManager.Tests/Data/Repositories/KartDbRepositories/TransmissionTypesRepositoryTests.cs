using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Data.Repositories.KartDbRepositories;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Data.Repositories.KartDbRepositories
{
    [TestFixture]
    public class TransmissionTypesRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IKartDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new TransmissionTypesRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IKartDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new TransmissionTypesRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyTransmissionTypesOnce()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new TransmissionTypesRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.TransmissionTypes, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnTransmissionTypesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new TransmissionTypesRepository(contextMock.Object);

            contextMock.Setup(x => x.TransmissionTypes.Add(It.IsAny<TransmissionType>()));

            var transmissionType = new TransmissionType();

            // Act

            sut.Add(transmissionType);

            // Assert

            contextMock.Verify(x => x.TransmissionTypes.Add(transmissionType), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnTransmissionTypesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new TransmissionTypesRepository(contextMock.Object);

            contextMock.Setup(x => x.TransmissionTypes.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.TransmissionTypes.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnTransmissionTypesPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IKartDbContext>();

            var sut = new TransmissionTypesRepository(contextMock.Object);

            contextMock.Setup(x => x.TransmissionTypes.Remove(It.IsAny<TransmissionType>()));

            var transmissionType = new TransmissionType();

            // Act

            sut.Remove(transmissionType);

            // Assert

            contextMock.Verify(x => x.TransmissionTypes.Remove(transmissionType), Times.Once);

        }

    }
}
