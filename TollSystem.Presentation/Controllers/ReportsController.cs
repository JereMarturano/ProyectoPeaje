using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;

namespace TollSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ITollQueryService _tollQueryService;

        public ReportsController(ITollQueryService tollQueryService)
        {
            _tollQueryService = tollQueryService;
        }

        [HttpGet("vehicles/{licensePlate}/passages")]
        public async Task<IActionResult> GetTollPassagesByVehicle(string licensePlate)
        {
            var passages = await _tollQueryService.GetTollPassagesByVehicleAsync(licensePlate);
            return Ok(passages);
        }
    }
}
