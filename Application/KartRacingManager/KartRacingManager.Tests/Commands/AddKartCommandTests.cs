using KarRacingManager.Models;
using KarRacingManager.Models.PostgreSqlModels;
using KartRacingManager.Commands.Commands;
using KartRacingManager.Data.Interfaces;
using KartRacingManager.Interfaces.Providers;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace KartRacingManager.Tests.Commands
{
    [TestFixture]
    public class AddKartCommandTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidKartsUnitOfWorkIsProvided()
        {
            // Arrange

            IKartsUnitOfWork kartsUnitOfWork = null;
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddKartCommand(kartsUnitOfWork, writerStub.Object, modelFactoryStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidWriterIsProvided()
        {
            // Arrange

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            IWriter writerStub = null;
            var modelFactoryStub = new Mock<IModelFactory>();

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub, modelFactoryStub.Object));
        }

        [Test]
        public void ConstructorShouldThrow_WhenInvalidModeFactoryIsProvided()
        {
            // Arrange

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            IModelFactory modelFactoryStub = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenAllParametersAreValid()
        {
            // Arrange

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            // Act & Assert

            Assert.DoesNotThrow(() => new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object));
        }

        [Test]
        public void ExecuteShouldDisplayCorrectMessageAndNotSaveKart_WhenNoParametersArePassed()
        {
            // Arrange
            var input = new string[] {  };

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert
            writerStub.Verify(x => x.Write("AddKart horsepower top_speed_in_km transmission_type"), Times.Once);
            writerStub.Verify(x => x.Write(Environment.NewLine), Times.Once);
            kartsUnitOfWorkStub.Verify(x => x.Save(), Times.Never);
        }

        [Test]
        public void ExecuteShouldDisplayCorrectMessageAndNotSaveKart_WhenParametersCountIsLessThanThree()
        {
            // Arrange
            var input = new string[] { "500", "360" };

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert
            writerStub.Verify(x => x.Write("Incorrect number of parameters."), Times.Once);
            writerStub.Verify(x => x.Write(Environment.NewLine), Times.Once);
            kartsUnitOfWorkStub.Verify(x => x.Save(), Times.Never);
        }

        [Test]
        public void ExecuteShouldDisplayCorrectMessageAndNotSaveKart_WhenInvalidHorsePowerIsProvided()
        {
            // Arrange
            var input = new string[] { "notHorsePower", "360", "noob" };

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert
            writerStub.Verify(x => x.Write("Horsepower must be integer."), Times.Once);
            writerStub.Verify(x => x.Write(Environment.NewLine), Times.Once);
            kartsUnitOfWorkStub.Verify(x => x.Save(), Times.Never);
        }

        [Test]
        public void ExecuteShouldDisplayCorrectMessageAndNotSaveKart_WhenInvalidTopSpeedIsProvided()
        {
            // Arrange
            var input = new string[] { "500", "notTopSpeed", "noob" };
            
            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert
            writerStub.Verify(x => x.Write("Top speed must be a number."), Times.Once);
            writerStub.Verify(x => x.Write(Environment.NewLine), Times.Once);
            kartsUnitOfWorkStub.Verify(x => x.Save(), Times.Never);

        }

        [Test]
        public void ExecuteShouldCreateTransmissionType_WhenNoTransmissionTypeIsFound()
        {
            // Arrange
            var input = new string[] { "500", "360", "noob" };

            var expectedHorsePower = int.Parse(input[0]);
            var expectedTopSpeed = double.Parse(input[1]);

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert

            modelFactoryStub.Verify(x => x.CreateTransmissionType(), Times.Once);
        }

        [Test]
        public void ExecuteShouldCreateTransmissionTypeWithCorrectName_WhenNoTransmissionTypeIsFound()
        {
            // Arrange
            var input = new string[] { "500", "360", "noob" };

            var expectedHorsePower = int.Parse(input[0]);
            var expectedTopSpeed = double.Parse(input[1]);

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert

            modelFactoryStub.Verify(x => x.CreateTransmissionType(), Times.Once);
            Assert.AreEqual(input[2], transmissionType.Name);
        }

        [Test]
        public void ExecuteShouldCreateKart_WhenAllParametersAreValid()
        {
            // Arrange
            var input = new string[] { "500", "360", "noob" };

            var expectedHorsePower = int.Parse(input[0]);
            var expectedTopSpeed = double.Parse(input[1]);

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert

            modelFactoryStub.Verify(x => x.CreateKart(), Times.Once);
        }

        [Test]
        public void ExecuteShouldCreateKartWithCorrectProperties_WhenAllParametersAreValid()
        {
            // Arrange
            var input = new string[] { "500", "360", "noob" };

            var expectedHorsePower = int.Parse(input[0]);
            var expectedTopSpeed = double.Parse(input[1]);

            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert

            modelFactoryStub.Verify(x => x.CreateKart(), Times.Once);
            Assert.AreEqual(expectedHorsePower, kart.HorsePower);
            Assert.AreEqual(expectedTopSpeed, kart.TopSpeedKph);
            Assert.AreEqual(transmissionType, kart.TransmissionType);

        }

        [Test]
        public void ExecuteShouldAddKartToKartsRepo_WhenAllParametersAreValid()
        {
            // Arrange
            var input = new string[] { "500", "360", "noob" };
            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert

            kartsUnitOfWorkStub.Verify(x => x.KartsRepo.Add(kart), Times.Once);
        }

        [Test]
        public void ExecuteShouldSaveChanges_WhenAllParametersAreValid()
        {
            // Arrange
            var input = new string[] { "500", "360", "noob" };
            var transmissionType = new TransmissionType();
            var kart = new Kart();
            var emptyIQueriable = Enumerable.Empty<TransmissionType>().AsQueryable();

            var kartsUnitOfWorkStub = new Mock<IKartsUnitOfWork>();
            var writerStub = new Mock<IWriter>();
            var modelFactoryStub = new Mock<IModelFactory>();

            var sut = new AddKartCommand(kartsUnitOfWorkStub.Object, writerStub.Object, modelFactoryStub.Object);

            writerStub.Setup(x => x.Write(It.IsAny<string>()));

            kartsUnitOfWorkStub.Setup(x => x.TransmissionTypesRepo.All).Returns(emptyIQueriable);
            kartsUnitOfWorkStub.Setup(x => x.KartsRepo.Add(kart));
            kartsUnitOfWorkStub.Setup(x => x.Save());

            modelFactoryStub.Setup(x => x.CreateTransmissionType()).Returns(transmissionType);
            modelFactoryStub.Setup(x => x.CreateKart()).Returns(kart);

            // Act

            sut.Execute(input);

            // Assert

            kartsUnitOfWorkStub.Verify(x => x.Save(), Times.Once);

        }

    }
}
