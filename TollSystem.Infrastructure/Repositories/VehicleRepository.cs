using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Infrastructure.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Vehicle> GetByLicensePlateAsync(LicensePlate licensePlate)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
        }
    }
}
