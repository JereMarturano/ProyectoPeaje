using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TollSystem.Integration.Tests
{
    public class ReportsControllerTests : IClassFixture<TollSystemApiFactory>
    {
        private readonly HttpClient _client;

        public ReportsControllerTests(TollSystemApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTollPassagesByVehicle_ShouldReturnOk()
        {
            // Arrange
            var licensePlate = "AA 123 BB";

            // Act
            var response = await _client.GetAsync($"/api/reports/vehicles/{licensePlate}/passages");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
