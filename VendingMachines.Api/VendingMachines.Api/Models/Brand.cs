using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
