using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
