using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Enums;
using TollSystem.Domain.Repositories;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Infrastructure.Repositories
{
    public class TariffRepository : Repository<Tariff>, ITariffRepository
    {
        public TariffRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Tariff> GetByCategoryAsync(VehicleCategory category)
        {
            return await _context.Tariffs.FirstOrDefaultAsync(t => t.VehicleCategory == category);
        }
    }
}
