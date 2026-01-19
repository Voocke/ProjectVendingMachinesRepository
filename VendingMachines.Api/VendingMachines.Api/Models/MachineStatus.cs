using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class MachineStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
