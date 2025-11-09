using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Application.Services;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;
using Xunit;

namespace TollSystem.Application.Tests
{
    public class TollPassageServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITollPassageRepository> _tollPassageRepositoryMock;
        private readonly Mock<ITariffService> _tariffServiceMock;
        private readonly Mock<ILogger<TollPassageService>> _loggerMock;
        private readonly TollPassageService _tollPassageService;

        public TollPassageServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _tollPassageRepositoryMock = new Mock<ITollPassageRepository>();
            _tariffServiceMock = new Mock<ITariffService>();
            _loggerMock = new Mock<ILogger<TollPassageService>>();
            _unitOfWorkMock.Setup(uow => uow.TollPassages).Returns(_tollPassageRepositoryMock.Object);
            _tollPassageService = new TollPassageService(_unitOfWorkMock.Object, _tariffServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateTollPassageAsync_ShouldCreateTollPassageWithCorrectAmount()
        {
            // Arrange
            var vehicle = new Vehicle(new LicensePlate("AA 123 BB"), "Red", 2, 1.5m, false);
            var expectedAmount = 100.0m;
            _tariffServiceMock.Setup(ts => ts.GetFeeForVehicle(vehicle)).ReturnsAsync(expectedAmount);

            // Act
            var result = await _tollPassageService.CreateTollPassageAsync(vehicle);

            // Assert
            Assert.Equal(vehicle, result.Vehicle);
            Assert.Equal(expectedAmount, result.AmountCharged);
            _tollPassageRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<TollPassage>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}
