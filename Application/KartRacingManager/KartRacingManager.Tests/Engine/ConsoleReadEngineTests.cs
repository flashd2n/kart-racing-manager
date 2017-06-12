﻿using KartRacingManager.Commands.Commands;
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

        [Test]
        public void RunShouldWriteCommandExactlyOnce_WhenOnlyExitCommandIsPassed()
        {
            // Arrange
            var expectedString = "Command: ";
            var inputRead = "exit";

            var readerStub = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.Setup(x => x.Read()).Returns(inputRead);

            commandFactoryStub.Setup(x => x.GetCommand(inputRead)).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerMock.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            writerMock.Verify(x => x.Write(expectedString), Times.Once);
        }

        [Test]
        public void RunShouldReadExactleOnce_WhenOnlyExitCommandIsPassed()
        {
            // Arrange
            var inputRead = "exit";

            var readerMock = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerMock.Setup(x => x.Read()).Returns(inputRead);

            commandFactoryStub.Setup(x => x.GetCommand(inputRead)).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerMock.Object, writerStub.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            readerMock.Verify(x => x.Read(), Times.Once);
        }

        [Test]
        public void RunShouldNotCallCommandFactory_WhenEmptyCommandIsPassed()
        {
            // Arrange
            var inputExit = "exit";

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.SetupSequence(x => x.Read()).Returns(String.Empty).Returns(inputExit);

            commandFactoryMock.Setup(x => x.GetCommand(It.IsAny<string>())).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactoryMock.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            commandFactoryMock.Verify(x => x.GetCommand(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RunShouldCallCommandFactory_WhenNonEmptyCommandIsPassed()
        {
            // Arrange
            var inputExit = "exit";
            var input = "inputMe";

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.SetupSequence(x => x.Read()).Returns(input).Returns(inputExit);

            commandFactoryMock.SetupSequence(x => x.GetCommand(It.IsAny<string>())).Returns(null).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactoryMock.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            commandFactoryMock.Verify(x => x.GetCommand(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RunShouldNotWrite_WhenExitCommandIsPassed()
        {
            // Arrange
            var inputExit = "exit";

            var readerStub = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.Setup(x => x.Read()).Returns(inputExit);

            commandFactoryStub.Setup(x => x.GetCommand(It.IsAny<string>())).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerMock.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            writerMock.Verify(x => x.Write(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RunShouldWriteUnrecognizedCommand_WhenInvalidCommandIsPassed()
        {
            // Arrange
            var inputExit = "exit";
            var invalidCommand = "inputMe";
            var expectedOutputMessage = $"Unrecognized command \"{invalidCommand}\"";

            var readerStub = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.SetupSequence(x => x.Read()).Returns(invalidCommand).Returns(inputExit);

            commandFactoryStub.SetupSequence(x => x.GetCommand(It.IsAny<string>())).Returns(null).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerMock.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            writerMock.Verify(x => x.Write(expectedOutputMessage), Times.Once);
        }

        [Test]
        public void RunShouldLogUnrecognizedCommand_WhenInvalidCommandIsPassed()
        {
            // Arrange
            var inputExit = "exit";
            var invalidCommand = "inputMe";
            var expectedOutputMessage = $"Unrecognized command \"{invalidCommand}\".";
            var logType = "info";

            var readerStub = new Mock<IReader>();
            var writerStib = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerMock = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.SetupSequence(x => x.Read()).Returns(invalidCommand).Returns(inputExit);

            commandFactoryStub.SetupSequence(x => x.GetCommand(It.IsAny<string>())).Returns(null).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerStib.Object, commandFactoryStub.Object, loggerMock.Object);

            // Act
            sut.Run();

            // Assert

            loggerMock.Verify(x => x.Log(logType, expectedOutputMessage));
        }

        [Test]
        public void RunShouldWriteNewLineAfterWritingUnrecognizedCommand_WhenInvalidCommandIsPassed()
        {
            // Arrange
            var inputExit = "exit";
            var invalidCommand = "inputMe";
            var expectedOutputMessage = $"Unrecognized command \"{invalidCommand}\"";

            var readerStub = new Mock<IReader>();
            var writerMock = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var exitCommandStub = new Mock<ICommand>();

            readerStub.SetupSequence(x => x.Read()).Returns(invalidCommand).Returns(inputExit);

            commandFactoryStub.SetupSequence(x => x.GetCommand(It.IsAny<string>())).Returns(null).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerMock.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            writerMock.Verify(x => x.Write(expectedOutputMessage), Times.Once);
            writerMock.Verify(x => x.Write(Environment.NewLine), Times.Once);
        }

        [Test]
        public void RunShouldCallCommandExecuteOnceWithArgumentStringArray_WhenPassedValidCommandFromConsole()
        {
            // Arrange
            var inputExit = "exit";
            var validCommand = "addracer";

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var validCommadMock = new Mock<ICommand>();
            var exitCommandStub = new Mock<ICommand>();

            validCommadMock.Setup(x => x.Execute(It.IsAny<string[]>()));

            readerStub.SetupSequence(x => x.Read()).Returns(validCommand).Returns(inputExit);

            commandFactoryStub.SetupSequence(x => x.GetCommand(It.IsAny<string>())).Returns(validCommadMock.Object).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            validCommadMock.Verify(x => x.Execute(It.IsAny<string[]>()), Times.Once);
        }
        
        [Test]
        public void RunShouldCallCommandExecuteWithCorrectArgumentOnce_WhenPassedCommandArgumentsHaveNoQuotes()
        {
            // Arrange
            var inputExit = "exit";
            var validCommand = "addracer";

            var readerStub = new Mock<IReader>();
            var writerStub = new Mock<IWriter>();
            var commandFactoryStub = new Mock<ICommandFactory>();
            var loggerStub = new Mock<ILogger>();

            var validCommadMock = new Mock<ICommand>();
            var exitCommandStub = new Mock<ICommand>();

            validCommadMock.Setup(x => x.Execute(It.IsAny<string[]>()));

            readerStub.SetupSequence(x => x.Read()).Returns(validCommand).Returns(inputExit);

            commandFactoryStub.SetupSequence(x => x.GetCommand(It.IsAny<string>())).Returns(validCommadMock.Object).Returns(exitCommandStub.Object);

            var sut = new ConsoleReadEngine(readerStub.Object, writerStub.Object, commandFactoryStub.Object, loggerStub.Object);

            // Act
            sut.Run();

            // Assert

            validCommadMock.Verify(x => x.Execute(It.IsAny<string[]>()), Times.Once);
        }

        [Test]
        public void RunShouldCallCommandExecuteWithCorrectArgumentOnce_WhenPassedCommandHasOneArgumentWithQuotes()
        {

        }

        [Test]
        public void RunShouldCallCommandExecuteWithCorrectArgumentOnce_WhenPassedCommandHasOneArgumentWithQuotesAndEscapedQuoteInside()
        {

        }

        [Test]
        public void RunShouldCallCommandExecuteWithCorrectArgumentOnce_WhenPassedCommandHasOneArgumentWithQuotesAndEscapedSlashInside()
        {

        }

        [Test]
        public void RunShouldCallCommandExecuteWithCorrectArgumentOnce_WhenPassedCommandHasAllArgumentsWithQuotes()
        {

        }

        [Test]
        public void RunShouldLogCommandExecution_WhenPassedValidCommandFromConsole()
        {

        }

        [Test]
        public void RunShouldWriteErrorMessage_WhenCommandThrows()
        {

        }

        [Test]
        public void RunShouldWriteErrorMessageAndNewLine_WhenCommandThrows()
        {

        }

        [Test]
        public void RunShouldLogDetailedErrorMessage_WhenCommandThrows()
        {

        }



    }
}