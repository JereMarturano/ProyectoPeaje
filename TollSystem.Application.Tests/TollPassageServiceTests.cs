using Moq;
using System.Threading.Tasks;
using TollSystem.Application.Services;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using Xunit;

namespace TollSystem.Application.Tests
{
    public class TollPassageServiceTests
    {
        [Fact]
        public async Task CreateTollPassageAsync_ShouldCreateTollPassage()
        {
            // Arrange
            var tollPassageRepositoryMock = new Mock<ITollPassageRepository>();
            var vehicle = new Vehicle("ABC-123", "Red", 2);
            var tollPassage = new TollPassage(vehicle);
            tollPassageRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<TollPassage>())).ReturnsAsync(tollPassage);

            var tollPassageService = new TollPassageService(tollPassageRepositoryMock.Object);

            // Act
            var result = await tollPassageService.CreateTollPassageAsync(vehicle);

            // Assert
            Assert.Equal(vehicle, result.Vehicle);
            tollPassageRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<TollPassage>()), Times.Once);
        }
    }
}
