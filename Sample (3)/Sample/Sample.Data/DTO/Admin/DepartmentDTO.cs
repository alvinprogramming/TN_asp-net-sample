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
    public class DepartmentDTO
    {  

        
        public DepartmentDTO()
        {

        }

        public DepartmentDTO(DepartmentInfo departmentInfo)
        {
            this.DepartmentID = departmentInfo.DepartmentID;
            this.DepartmentName = departmentInfo.DepartmentName;
            this.CreatedBy = departmentInfo.CreatedBy;
            this.CreatedDate = departmentInfo.CreatedDate;
            this.UpdatedBy = departmentInfo.UpdatedBy;
            this.UpdatedDate = departmentInfo.UpdatedDate;
            this.IsEnabled = departmentInfo.IsEnabled;
        }

        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; } = null!;

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsEnabled { get; set; }
    }
}
