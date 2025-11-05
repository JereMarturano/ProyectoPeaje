using Moq;
using System.Threading.Tasks;
using TollSystem.Application.Services;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using Xunit;

namespace TollSystem.Application.Tests
{
    public class VehicleServiceTests
    {
        [Fact]
        public async Task GetOrCreateVehicleAsync_ShouldCreateVehicle()
        {
            // Arrange
            var vehicleRepositoryMock = new Mock<IVehicleRepository>();
            var vehicle = new Vehicle("ABC-123", "Red", 2);
            vehicleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Vehicle>())).ReturnsAsync(vehicle);

            var vehicleService = new VehicleService(vehicleRepositoryMock.Object);

            // Act
            var result = await vehicleService.GetOrCreateVehicleAsync("ABC-123", "Red", 2);

            // Assert
            Assert.Equal("ABC-123", result.LicensePlate);
            Assert.Equal("Red", result.Color);
            Assert.Equal(2, result.Axles);
            vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>()), Times.Once);
        }
    }
}
