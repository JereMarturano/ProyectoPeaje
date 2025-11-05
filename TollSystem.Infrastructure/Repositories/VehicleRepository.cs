using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Infrastructure.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
