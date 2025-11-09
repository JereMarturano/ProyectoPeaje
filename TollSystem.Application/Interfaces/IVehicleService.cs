using System.Threading.Tasks;
using TollSystem.Domain.Entities;

namespace TollSystem.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> GetOrCreateVehicleAsync(string licensePlate, string color, int axles, decimal height, bool hasDualWheels);
    }
}
