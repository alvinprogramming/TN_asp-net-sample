using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.IServices
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAllAsync ();
        Task<T> GetbyIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
