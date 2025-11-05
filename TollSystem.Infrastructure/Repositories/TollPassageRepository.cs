using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Infrastructure.Repositories
{
    public class TollPassageRepository : Repository<TollPassage>, ITollPassageRepository
    {
        public TollPassageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
