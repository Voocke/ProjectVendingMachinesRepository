using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal Price { get; set; }

    public int MinimumStock { get; set; }

    public decimal? SalesTendencyPerDay { get; set; }

    public virtual ICollection<MachineProductStock> MachineProductStocks { get; set; } = new List<MachineProductStock>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
