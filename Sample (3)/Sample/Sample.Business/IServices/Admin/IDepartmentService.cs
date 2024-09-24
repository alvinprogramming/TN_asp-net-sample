using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;

namespace Sample.Business.IServices.Admin
{
    public interface IDepartmentService : IBaseService<DepartmentDTO>
    {
        /*serDTO GetUserDTO(int id);*/

        Task<DepartmentInfo> GetById(Guid id);
        Task<List<DepartmentDTO>> Search(string emailAddress);
    }
}
