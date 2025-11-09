using System.Collections.Generic;
using System.Threading.Tasks;
using TollSystem.Domain.Entities;

namespace TollSystem.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
