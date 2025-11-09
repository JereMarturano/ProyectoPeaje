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
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _unitOfWorkMock.Setup(uow => uow.Vehicles).Returns(_vehicleRepositoryMock.Object);
            _vehicleService = new VehicleService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetOrCreateVehicleAsync_WhenVehicleExists_ShouldReturnExistingVehicle()
        {
            // Arrange
            var licensePlate = new LicensePlate("AA 123 BB");
            var vehicle = new Vehicle(licensePlate, "Red", 2, 1.5m, false);
            _vehicleRepositoryMock.Setup(repo => repo.GetByLicensePlateAsync(It.Is<LicensePlate>(lp => lp.Value == licensePlate.Value))).ReturnsAsync(vehicle);

            // Act
            var result = await _vehicleService.GetOrCreateVehicleAsync("AA 123 BB", "Red", 2, 1.5m, false);

            // Assert
            Assert.Equal(vehicle, result);
            _vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>()), Times.Never);
        }

        [Fact]
        public async Task GetOrCreateVehicleAsync_WhenVehicleDoesNotExist_ShouldCreateAndReturnNewVehicle()
        {
            // Arrange
            var licensePlate = new LicensePlate("AA 123 BB");
            _vehicleRepositoryMock.Setup(repo => repo.GetByLicensePlateAsync(It.Is<LicensePlate>(lp => lp.Value == licensePlate.Value))).ReturnsAsync((Vehicle)null);

            // Act
            var result = await _vehicleService.GetOrCreateVehicleAsync("AA 123 BB", "Blue", 4, 2.5m, true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AA 123 BB", result.LicensePlate.Value);
            _vehicleRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Vehicle>()), Times.Once);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("AA 123 B")]
        [InlineData(null)]
        public async Task GetOrCreateVehicleAsync_WithInvalidLicensePlate_ShouldThrowException(string invalidPlate)
        {
            // Act & Assert
            await Assert.ThrowsAsync<InvalidLicensePlateFormatException>(() =>
                _vehicleService.GetOrCreateVehicleAsync(invalidPlate, "Red", 2, 1.5m, false));
        }
    }
}
