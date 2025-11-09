using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Presentation.DTOs;

namespace TollSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TollPassagesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly ITollPassageService _tollPassageService;

        public TollPassagesController(IVehicleService vehicleService, ITollPassageService tollPassageService)
        {
            _vehicleService = vehicleService;
            _tollPassageService = tollPassageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTollPassage(CreateTollPassageDto dto)
        {
            var vehicle = await _vehicleService.GetOrCreateVehicleAsync(dto.LicensePlate, dto.Color, dto.Axles, dto.Height, dto.HasDualWheels);
            var tollPassage = await _tollPassageService.CreateTollPassageAsync(vehicle);
            return Ok(tollPassage);
        }
    }
}
