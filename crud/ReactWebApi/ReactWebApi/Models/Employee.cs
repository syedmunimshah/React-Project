using System;
using System.Collections.Generic;

namespace ReactWebApi.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Age { get; set; } = null!;

    public bool? IsActive { get; set; }
}
