using System.Threading.Tasks;
using TollSystem.Domain.Entities;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Domain.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Vehicle> GetByLicensePlateAsync(LicensePlate licensePlate);
    }
}
