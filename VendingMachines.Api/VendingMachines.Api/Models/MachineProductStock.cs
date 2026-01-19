using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class MachineProductStock
{
    public int VendingMachineId { get; set; }

    public int ProductId { get; set; }

    public int QuantityInStock { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
