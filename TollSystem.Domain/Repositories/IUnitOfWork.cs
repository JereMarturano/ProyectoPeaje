using System.Threading.Tasks;

namespace TollSystem.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IVehicleRepository Vehicles { get; }
        ITollPassageRepository TollPassages { get; }
        ITariffRepository Tariffs { get; }
        Task<int> CompleteAsync();
    }
}
