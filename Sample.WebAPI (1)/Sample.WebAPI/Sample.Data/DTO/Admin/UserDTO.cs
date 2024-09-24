using Sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.DTO.Admin
{
    public class UserDTO
    {
        public UserDTO() 
        { 
        }


        public UserDTO(UserInfo user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            FullName = user.MiddleName != null ? $"{user.LastName}, {user.FirstName} {user.MiddleName}" : $"{user.LastName}, {user.FirstName}";
            Age = user.Age;
            EmailAddress = user.EmailAddress;
            Address = user.Address;
            CreatedBy = user.CreatedBy;
            CreatedDate = user.CreatedDate.ToString("MM/dd/yyyy");
            UpdatedBy = user.UpdatedBy;
            UpdatedDate = user.UpdatedDate?.ToString("MM/dd/yyyy");
            IsEnabled = user.IsEnabled;
        }



        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? EmailAddress { get; set; }
        public string Address { get; set; } = null!;
        public Guid CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; } = null!;
        public bool IsEnabled { get; set; }
    }
}
