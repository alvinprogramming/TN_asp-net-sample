using System;
using System.Collections.Generic;

namespace Sample.Data.Entities;

public partial class UserInfo
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public string? EmailAddress { get; set; }

    public string Address { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsEnabled { get; set; }
}
