using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Data.Entities;

public partial class DepartmentInfo   
{
    [Key]
    public Guid DepartmentID { get; set; }
    public string DepartmentName { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsEnabled { get; set; }


}
