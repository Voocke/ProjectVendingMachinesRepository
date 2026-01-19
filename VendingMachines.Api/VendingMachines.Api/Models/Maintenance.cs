using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public int VendingMachineId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public string WorkDescription { get; set; } = null!;

    public string? Problems { get; set; }

    public int ExecutorUserId { get; set; }

    public int? DurationHours { get; set; }

    public virtual User ExecutorUser { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
