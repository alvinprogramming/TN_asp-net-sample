using Azure;
using Sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.DTO.Admin
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            Username = "admin";
            Password = "password";
        }


        //public LoginDTO(UserInfo user)
        //{
        //    Username = user.FirstName;
        //    Password = user.LastName;
        //}

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
