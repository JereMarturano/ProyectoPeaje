using Moq;
using System.Threading.Tasks;
using TollSystem.Application.Services;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Exceptions;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;
using Xunit;

namespace TollSystem.Application.Tests
{
    public class VehicleServiceTests
    {
        [Theory]
        [InlineData("AA 123 BB")]
        [InlineData("AAA 123")]
        public async Task GetOrCreateVehicleAsync_WithValidLicensePlate_ShouldCreateVehicle(string validPlate)
        {
            // Arrange
            var vehicleRepositoryMock = new Mock<IVehicleRepository>();
            var vehicle = new Vehicle(new LicensePlate(validPlate), "Red", 2);
            vehicleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Vehicle>())).ReturnsAsync(vehicle);

            var vehicleService = new VehicleService(vehicleRepositoryMock.Object);

            // Act
            var result = await vehicleService.GetOrCreateVehicleAsync(validPlate, "Red", 2);

            // Assert
            Assert.Equal(validPlate, result.LicensePlate.Value);
            vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>()), Times.Once);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("AA 123 B")]
        [InlineData("AAA 1234")]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetOrCreateVehicleAsync_WithInvalidLicensePlate_ShouldThrowException(string invalidPlate)
        {
            // Arrange
            var vehicleRepositoryMock = new Mock<IVehicleRepository>();
            var vehicleService = new VehicleService(vehicleRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidLicensePlateFormatException>(() =>
                vehicleService.GetOrCreateVehicleAsync(invalidPlate, "Red", 2));
        }
    }
}
