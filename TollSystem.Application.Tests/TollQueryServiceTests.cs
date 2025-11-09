using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TollSystem.Application.Services;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;
using Xunit;

namespace TollSystem.Application.Tests
{
    public class TollQueryServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITollPassageRepository> _tollPassageRepositoryMock;
        private readonly TollQueryService _tollQueryService;

        public TollQueryServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _tollPassageRepositoryMock = new Mock<ITollPassageRepository>();
            _unitOfWorkMock.Setup(uow => uow.TollPassages).Returns(_tollPassageRepositoryMock.Object);
            _tollQueryService = new TollQueryService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetTollPassagesByVehicleAsync_ShouldReturnTollPassages()
        {
            // Arrange
            var licensePlate = new LicensePlate("AA 123 BB");
            var passages = new List<TollPassage> { new TollPassage(new Vehicle(licensePlate, "Red", 2, 1.5m, false), 100) };
            _tollPassageRepositoryMock.Setup(repo => repo.GetByLicensePlateAsync(It.Is<LicensePlate>(lp => lp.Value == licensePlate.Value))).ReturnsAsync(passages);

            // Act
            var result = await _tollQueryService.GetTollPassagesByVehicleAsync("AA 123 BB");

            // Assert
            Assert.Equal(passages, result);
        }

        [Fact]
        public async Task GetTollPassagesByDateRangeAsync_ShouldReturnTollPassages()
        {
            // Arrange
            var start = DateTime.UtcNow.AddDays(-1);
            var end = DateTime.UtcNow;
            var passages = new List<TollPassage> { new TollPassage(new Vehicle(new LicensePlate("AA 123 BB"), "Red", 2, 1.5m, false), 100) };
            _tollPassageRepositoryMock.Setup(repo => repo.GetByDateRangeAsync(start, end)).ReturnsAsync(passages);

            // Act
            var result = await _tollQueryService.GetTollPassagesByDateRangeAsync(start, end);

            // Assert
            Assert.Equal(passages, result);
        }
    }
}
