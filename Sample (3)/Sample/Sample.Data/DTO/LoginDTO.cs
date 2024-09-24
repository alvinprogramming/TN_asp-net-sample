using Sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.DTO
{
    public class LoginDTO
    {
        public LoginDTO()
        {

        }



        public LoginDTO(UserInfo user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
        }

        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
