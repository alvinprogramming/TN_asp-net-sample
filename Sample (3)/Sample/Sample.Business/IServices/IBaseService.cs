using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.IServices
{
    public interface IBaseService<T>
    {

        // NOTE : The base interface. Interfaces are used primarily for ease of backtracking in terms of return values and its parameters.
            // 
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);


        

        

    }
}
