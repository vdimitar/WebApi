
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WebApi.IServices
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
