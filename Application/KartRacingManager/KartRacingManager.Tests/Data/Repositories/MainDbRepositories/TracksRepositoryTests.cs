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
    public class TracksRepositoryTests
    {
        [Test]
        public void ConstructorShouldThrow_WhenInvalidContextIsPassed()
        {
            // Arrange
            IMainDbContext context = null;

            // Act & Assert

            Assert.Throws<ArgumentNullException>(() => new TracksRepository(context));
        }

        [Test]
        public void ConstructorShouldNotThrow_WhenValidContextIsPassed()
        {
            var context = new Mock<IMainDbContext>();

            // Act & Assert

            Assert.DoesNotThrow(() => new TracksRepository(context.Object));
        }

        [Test]
        public void AllPropertyShouldCallContextPropertyTracksOnce()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new TracksRepository(contextMock.Object);

            // Act

            var result = sut.All;

            // Assert

            contextMock.Verify(x => x.Tracks, Times.Once);
        }

        [Test]
        public void AddShouldCallContextAddMethodOnTracksPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new TracksRepository(contextMock.Object);

            contextMock.Setup(x => x.Tracks.Add(It.IsAny<Track>()));

            var track = new Track();

            // Act

            sut.Add(track);

            // Assert

            contextMock.Verify(x => x.Tracks.Add(track), Times.Once);
        }

        [Test]
        public void FindByIdShouldCallContextFindMethodOnTracksPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new TracksRepository(contextMock.Object);

            contextMock.Setup(x => x.Tracks.Find(It.IsAny<object>()));

            var id = 7;

            // Act

            sut.FindById(id);

            // Assert

            contextMock.Verify(x => x.Tracks.Find(id), Times.Once);
        }

        [Test]
        public void RemoveShouldCallContextRemoveMethodOnTracksPropertyOnce_WithCorrectParameter()
        {
            // Arrange

            var contextMock = new Mock<IMainDbContext>();

            var sut = new TracksRepository(contextMock.Object);

            contextMock.Setup(x => x.Tracks.Remove(It.IsAny<Track>()));

            var track = new Track();

            // Act

            sut.Remove(track);

            // Assert

            contextMock.Verify(x => x.Tracks.Remove(track), Times.Once);

        }
    }
}
