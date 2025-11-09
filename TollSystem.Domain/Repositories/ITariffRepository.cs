using System.Threading.Tasks;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Enums;

namespace TollSystem.Domain.Repositories
{
    public interface ITariffRepository : IRepository<Tariff>
    {
        Task<Tariff> GetByCategoryAsync(VehicleCategory category);
    }
}
