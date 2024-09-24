using Sample.Data.DTO.Admin;
using Sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.IServices.Admin
{
    public interface IUserService : IBaseService<UserDTO>
    {
        Task<UserInfo> GetbyId (Guid id);
        Task<List<UserDTO>> Search(string emailAddress);
    }
}
