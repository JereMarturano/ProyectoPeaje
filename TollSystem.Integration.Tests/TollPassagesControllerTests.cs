using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TollSystem.Presentation.DTOs;
using Xunit;

namespace TollSystem.Integration.Tests
{
    public class TollPassagesControllerTests : IClassFixture<TollSystemApiFactory>
    {
        private readonly HttpClient _client;

        public TollPassagesControllerTests(TollSystemApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostTollPassage_ShouldReturnOk()
        {
            // Arrange
            var dto = new CreateTollPassageDto
            {
                LicensePlate = "AA 123 BB",
                Color = "Red",
                Axles = 2,
                Height = 1.5m,
                HasDualWheels = false
            };
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/tollpassages", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
