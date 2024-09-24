using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Validators.IValidators
{
    public interface IBaseValidator<T>
    {
        Task<List<(int, string)>> ValidateAsync(T entity, HttpMethod httpMethod);
    }
}
