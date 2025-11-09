using System.Threading.Tasks;
using TollSystem.Domain.Entities;

namespace TollSystem.Application.Interfaces
{
    public interface ITariffService
    {
        Task<decimal> GetFeeForVehicle(Vehicle vehicle);
    }
}
