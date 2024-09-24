using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Models
{
    public class APIResponse<T>
    {
        // dto for api response
        // if success, gets result 

        public bool Success { get; set; }
        public T? Result { get; set; }
    }
}
