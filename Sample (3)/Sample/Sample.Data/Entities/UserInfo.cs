using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Sample.Data.Entities;

public partial class UserInfo   
{
    
    public Guid Id { get; set; }
    // WARN : Primary key MUST be named with "Id" for automatic identification/insertion of the attribute [Key]. Else, add [Key] attribute

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public string? EmailAddress { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsEnabled { get; set; }

    public string Address { get; set; }

    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    // NOTE : will output error if fields is not 1:1 to database
}
