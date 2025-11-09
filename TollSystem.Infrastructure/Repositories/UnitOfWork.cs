using System;
using System.Threading.Tasks;
using TollSystem.Domain.Repositories;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IVehicleRepository Vehicles { get; }
        public ITollPassageRepository TollPassages { get; }
        public ITariffRepository Tariffs { get; }

        public UnitOfWork(ApplicationDbContext context, IVehicleRepository vehicleRepository, ITollPassageRepository tollPassageRepository, ITariffRepository tariffRepository)
        {
            _context = context;
            Vehicles = vehicleRepository;
            TollPassages = tollPassageRepository;
            Tariffs = tariffRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
