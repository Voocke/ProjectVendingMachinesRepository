using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int VendingMachineId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal SaleAmount { get; set; }

    public DateTime SaleDateTime { get; set; }

    public int PaymentMethodId { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
