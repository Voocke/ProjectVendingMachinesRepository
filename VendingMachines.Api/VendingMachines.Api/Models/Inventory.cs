using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int VendingMachineId { get; set; }

    public DateOnly InventoryDate { get; set; }

    public int UserId { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
