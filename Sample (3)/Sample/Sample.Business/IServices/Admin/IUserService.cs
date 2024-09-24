using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Data.DTO;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;

namespace Sample.Business.IServices.Admin
{
    public interface IUserService:IBaseService<UserDTO>
    {

        // NOTE : Interface for the chosen service. 
        // NOTE : To use the service, you call this interface inside the chosen service/controller
            // Make sure to 


        /*UserDTO GetUserDTO(int id);*/

        Task<UserInfo> GetById(Guid id);
        Task<List<UserDTO>> Search(string emailAddress);

        Task<LoginDTO> PreLogin(string username, string password);
    }
}
