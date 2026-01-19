using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class MachineType
{
    public int MachineTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
