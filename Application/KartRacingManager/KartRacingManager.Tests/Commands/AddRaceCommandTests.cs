using KarRacingManager.Models;
using KartRacingManager.Commands.Commands;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Providers;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Commands
{
    [TestFixture]
    public class AddRaceCommandTests
    {

        [Test]
        public void ConstructorShouldThrow_WhenInvalidMainUnitOfWorkIsProvided()
        {
            // Arrange

            IMainUnitOfWork mainUnitOfWorkStub = null;
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddRaceCommand(mainUnitOfWorkStub, writerStub.Object, modelFactoryStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidWriterIsProvided()
        {
            // Arrange

            var mainUnitOfWorkStub = new Mock<IMainUnitOfWork>();
            IWriter writerStub = null;
            var modelFactoryStub = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddRaceCommand(mainUnitOfWorkStub.Object, writerStub, modelFactoryStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidModelFactoryIsProvided()
        {
            // Arrange

            var mainUnitOfWorkStub = new Mock<IMainUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            IModelFactory modelFactoryStub = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddRaceCommand(mainUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenAllParametersAreValid()
        {
            // Arrange

            var mainUnitOfWorkStub = new Mock<IMainUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            // Act & Assert

            Assert.DoesNotThrow(() => new AddRaceCommand(mainUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object));

        }

    }
}
