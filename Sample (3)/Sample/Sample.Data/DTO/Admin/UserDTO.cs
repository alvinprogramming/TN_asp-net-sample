using Microsoft.EntityFrameworkCore;
using Sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.DTO.Admin
{
    public class UserDTO
    { 
        // NOTE : This setup (Constructor overriding) is required due to the whole application reading the DTOs meaning that it would require values (that is commonly not null) and would throw an error since the configuration in the DTO field is not null
        // EX : FirstName = user.FirstName
            // The config is set as FirstName is not nullable (FirstName != FirstName?)
            // user.FirstName, on initialization, is set as null
        public UserDTO()
        {

        }

        public UserDTO(UserInfo user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            FullName = user.MiddleName != null ? $"{user.LastName}, {user.FirstName} {user.MiddleName}" : $"{user.LastName}, {user.FirstName}";
            // (condition) if (true) : (false)
            Age = user.Age;
            Address = user.Address;
            EmailAddress = user.EmailAddress;
            CreatedBy = user.CreatedBy;
            CreatedDate = user.CreatedDate.ToString("MM/dd/yyyy");
            UpdatedBy = user.UpdatedBy;
            UpdatedDate = user.UpdatedDate?.ToString("MM/dd/yyyy");
            IsEnabled = user.IsEnabled;
            Username = user.Username;
            Password = user.Password;

            // WARN : is not advised in MBTC, to be declared and initialized in data access layer as LINQ instead
            //not safe and not proper due to (?)

            // NOTE : While doable, the acceptible format is setting the class fields as "null"
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? FullName { get; set; } = null!;

        public int Age { get; set; }

        public string? EmailAddress { get; set; }

        public Guid? CreatedBy { get; set; }

        public string? CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; } 

        public string? UpdatedDate { get; set; }

        public bool IsEnabled {  get; set; }

        public string Address { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        // NOTE : dto file is for calling records, window between database and business layer. access layer
        // the fields/records you will use/process from db to system
        // can be optional:
            // Get only what you need in a table
            // Get only what you need in multiple tables
            // Using the same table in multiple DTOs
    }
}
