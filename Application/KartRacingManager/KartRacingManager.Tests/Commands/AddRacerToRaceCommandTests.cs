using KartRacingManager.Commands.Commands;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Providers;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Commands
{
    [TestFixture]
    public class AddRacerToRaceCommandTests
    {

        [Test]
        public void ConstructorShouldThrow_WhenInvalidMainUnitOfWorkIsProvided()
        {
            // Arrange

            IMainUnitOfWork mainUnitOfWorkStub = null;
            var writerStub = new Mock<IWriter>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddRacerToRaceCommand(mainUnitOfWorkStub, writerStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidWriterIsProvided()
        {
            // Arrange

            var mainUnitOfWorkStub = new Mock<IMainUnitOfWork>();
            IWriter writerStub = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddRacerToRaceCommand(mainUnitOfWorkStub.Object, writerStub));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenAllParametersAreValid()
        {
            // Arrange

            var mainUnitOfWorkStub = new Mock<IMainUnitOfWork>();
            var writerStub = new Mock<IWriter>();

            // Act & Assert

            Assert.DoesNotThrow(() => new AddRacerToRaceCommand(mainUnitOfWorkStub.Object, writerStub.Object));

        }

    }
}
