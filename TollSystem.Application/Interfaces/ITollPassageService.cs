using System.Threading.Tasks;
using TollSystem.Domain.Entities;

namespace TollSystem.Application.Interfaces
{
    public interface ITollPassageService
    {
        Task<TollPassage> CreateTollPassageAsync(Vehicle vehicle);
    }
}
