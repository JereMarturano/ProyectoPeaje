using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;

namespace TollSystem.Application.Services
{
    public class TollPassageService : ITollPassageService
    {
        private readonly ITollPassageRepository _tollPassageRepository;

        public TollPassageService(ITollPassageRepository tollPassageRepository)
        {
            _tollPassageRepository = tollPassageRepository;
        }

        public async Task<TollPassage> CreateTollPassageAsync(Vehicle vehicle)
        {
            var tollPassage = new TollPassage(vehicle);
            return await _tollPassageRepository.AddAsync(tollPassage);
        }
    }
}
