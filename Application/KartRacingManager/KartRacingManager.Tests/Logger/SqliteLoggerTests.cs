using KarRacingManager.Models;
using KarRacingManager.Models.SqliteModels;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Providers;
using KartRacingManager.Logger;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace KartRacingManager.Tests.Logger
{
    [TestFixture]
    public class SqliteLoggerTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidLogsUnitOfWorkIsPassed()
        {
            // Arrage

            ILogsUnitOfWork logsUnitOfwork = null;
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new SqliteLogger(logsUnitOfwork, dateTimeProvider.Object, modelFactory.Object));

        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidDateTimeProviderIsPassed()
        {
            // Arrage

            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            IDateTimeProvider dateTimeProvider = null;
            var modelFactory = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider, modelFactory.Object));

        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidModelsFactoryIsPassed()
        {
            // Arrage

            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            IModelFactory modelFactory = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory));

        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidParametersArePassed()
        {
            // Arrage

            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            // Act & Assert

            Assert.DoesNotThrow(() => new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object));
        }

        [Test]
        public void LogShouldCreateNewLogType_WhenLogTypeIsNotFoundInDatabase()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var emptyIQueriable = Enumerable.Empty<LogType>().AsQueryable();

            var logType = new LogType();
            var log = new Log();
            
            var logTypeInput = "info";
            var logMessageInput = "some message";

            var expectedLogTime = new DateTime();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

            dateTimeProvider.Setup(x => x.UtcNow).Returns(expectedLogTime);

            logsUnitOfwork.Setup(x => x.LogTypesRepo.All).Returns(emptyIQueriable);
            logsUnitOfwork.Setup(x => x.LogsRepo.Add(It.IsAny<Log>()));
            logsUnitOfwork.Setup(x => x.Save());

            modelFactory.Setup(x => x.CreateLogType()).Returns(logType);
            modelFactory.Setup(x => x.CreateLog()).Returns(log);

            // Act

            sut.Log(logTypeInput, logMessageInput);

            // Assert

            modelFactory.Verify(x => x.CreateLogType(), Times.Once);

        }

        [Test]
        public void LogShouldCreateNewLogTypeInfo_WhenLogTypeIsNotFoundInDatabaseAndNoLogTypeIsProvided()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var emptyIQueriable = Enumerable.Empty<LogType>().AsQueryable();

            var logType = new LogType();
            var log = new Log();

            string logTypeInput = null;
            var logMessageInput = "some message";

            var expectedLogTime = new DateTime();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

            dateTimeProvider.Setup(x => x.UtcNow).Returns(expectedLogTime);

            logsUnitOfwork.Setup(x => x.LogTypesRepo.All).Returns(emptyIQueriable);
            logsUnitOfwork.Setup(x => x.LogsRepo.Add(It.IsAny<Log>()));
            logsUnitOfwork.Setup(x => x.Save());

            modelFactory.Setup(x => x.CreateLogType()).Returns(logType);
            modelFactory.Setup(x => x.CreateLog()).Returns(log);

            // Act

            sut.Log(logTypeInput, logMessageInput);

            // Assert

            Assert.AreEqual(logType.Name, "info");
        }

        [Test]
        public void LogShouldCreateNewLogInfoWithCorrectProepties_WhenValidTypeAndMessageArePassed()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var emptyIQueriable = Enumerable.Empty<LogType>().AsQueryable();

            var logType = new LogType();
            var log = new Log();

            string logTypeInput = "error";
            var logMessageInput = "some message";

            var expectedLogTime = new DateTime();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

            dateTimeProvider.Setup(x => x.UtcNow).Returns(expectedLogTime);

            logsUnitOfwork.Setup(x => x.LogTypesRepo.All).Returns(emptyIQueriable);
            logsUnitOfwork.Setup(x => x.LogsRepo.Add(It.IsAny<Log>()));
            logsUnitOfwork.Setup(x => x.Save());

            modelFactory.Setup(x => x.CreateLogType()).Returns(logType);
            modelFactory.Setup(x => x.CreateLog()).Returns(log);

            // Act

            sut.Log(logTypeInput, logMessageInput);

            // Assert

            Assert.AreEqual(log.Message, logMessageInput);
            Assert.AreEqual(log.LogType, logType);
            Assert.AreEqual(log.TimeStamp, expectedLogTime);
        }

        [Test]
        public void LogShouldCreateNewLogInfoWithEmptyMessage_WhenNoMessageIsPassed()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var emptyIQueriable = Enumerable.Empty<LogType>().AsQueryable();

            var logType = new LogType();
            var log = new Log();

            string logTypeInput = "error";
            string logMessageInput = null;

            var expectedLogTime = new DateTime();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

            dateTimeProvider.Setup(x => x.UtcNow).Returns(expectedLogTime);

            logsUnitOfwork.Setup(x => x.LogTypesRepo.All).Returns(emptyIQueriable);
            logsUnitOfwork.Setup(x => x.LogsRepo.Add(It.IsAny<Log>()));
            logsUnitOfwork.Setup(x => x.Save());

            modelFactory.Setup(x => x.CreateLogType()).Returns(logType);
            modelFactory.Setup(x => x.CreateLog()).Returns(log);

            // Act

            sut.Log(logTypeInput, logMessageInput);

            // Assert

            Assert.AreEqual(log.Message, "");
        }

        [Test]
        public void LogShouldAddLogToLogsRepo()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var emptyIQueriable = Enumerable.Empty<LogType>().AsQueryable();

            var logType = new LogType();
            var log = new Log();

            string logTypeInput = "error";
            string logMessageInput = "some message";

            var expectedLogTime = new DateTime();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

            dateTimeProvider.Setup(x => x.UtcNow).Returns(expectedLogTime);

            logsUnitOfwork.Setup(x => x.LogTypesRepo.All).Returns(emptyIQueriable);
            logsUnitOfwork.Setup(x => x.LogsRepo.Add(It.IsAny<Log>()));
            logsUnitOfwork.Setup(x => x.Save());

            modelFactory.Setup(x => x.CreateLogType()).Returns(logType);
            modelFactory.Setup(x => x.CreateLog()).Returns(log);

            // Act

            sut.Log(logTypeInput, logMessageInput);

            // Assert

            logsUnitOfwork.Verify(x => x.LogsRepo.Add(log), Times.Once);
        }

        [Test]
        public void LogShouldSaveTheChanges()
        {
            // Arrange
            var logsUnitOfwork = new Mock<ILogsUnitOfWork>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var modelFactory = new Mock<IModelFactory>();

            var emptyIQueriable = Enumerable.Empty<LogType>().AsQueryable();

            var logType = new LogType();
            var log = new Log();

            string logTypeInput = "error";
            string logMessageInput = "some message";

            var expectedLogTime = new DateTime();

            var sut = new SqliteLogger(logsUnitOfwork.Object, dateTimeProvider.Object, modelFactory.Object);

            dateTimeProvider.Setup(x => x.UtcNow).Returns(expectedLogTime);

            logsUnitOfwork.Setup(x => x.LogTypesRepo.All).Returns(emptyIQueriable);
            logsUnitOfwork.Setup(x => x.LogsRepo.Add(It.IsAny<Log>()));
            logsUnitOfwork.Setup(x => x.Save());

            modelFactory.Setup(x => x.CreateLogType()).Returns(logType);
            modelFactory.Setup(x => x.CreateLog()).Returns(log);

            // Act

            sut.Log(logTypeInput, logMessageInput);

            // Assert

            logsUnitOfwork.Verify(x => x.Save(), Times.Once);
        }
    }
}
