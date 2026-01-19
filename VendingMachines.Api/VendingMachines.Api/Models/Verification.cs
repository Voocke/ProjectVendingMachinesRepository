using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Verification
{
    public int VerificationId { get; set; }

    public int VendingMachineId { get; set; }

    public DateOnly VerificationDate { get; set; }

    public int UserId { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
