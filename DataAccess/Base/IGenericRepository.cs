using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string code);
        T GetById(Guid code);
        Task<T> GetByIdAsync(Guid code);

        // CRUD operations
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);

        // Save operation
        Task<int> SaveAsync();
    }
}
