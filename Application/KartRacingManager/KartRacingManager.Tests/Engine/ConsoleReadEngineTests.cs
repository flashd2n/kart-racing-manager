using KartRacingManager.Engine;
using KartRacingManager.Interfaces.Commands;
using KartRacingManager.Interfaces.Logger;
using KartRacingManager.Interfaces.Providers;
using Moq;
using NUnit.Framework;
using System;

namespace KartRacingManager.Tests.Engine
{
    [TestFixture]
    public class ConsoleReadEngineTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidReaderIsPassed()
        {
            // Arrange

            IReader reader = null;
            var writerStub = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new ConsoleReadEngine(reader, writerStub.Object, commandFactoryStub.Object, loggerStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidWriterrIsPassed()
        {
            // Arrange

            var readerStub = new Mock<IReader>();
            IWriter writer = null;
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new ConsoleReadEngine(readerStub.Object, writer, commandFactoryStub.Object, loggerStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidCommandFactoryIsPassed()
        {
            // Arrange

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            ICommandFactory commandFactory = null;
            var loggerStub = new Mock<ILogger>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactory, loggerStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidLoggerIsPassed()
        {
            // Arrange

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            ILogger logger = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactoryStub.Object, logger));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            // Act & Assert

            Assert.DoesNotThrow(() => new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactoryStub.Object, loggerStub.Object));
        }
    }
}
