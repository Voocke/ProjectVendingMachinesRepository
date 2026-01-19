using System;
using System.Collections.Generic;

namespace VendingMachines.Api.Models;

public partial class VendingMachine
{
    public int VendingMachineId { get; set; }

    public string Location { get; set; } = null!;

    public int ModelId { get; set; }

    public int MachineTypeId { get; set; }

    public int ManufacturerCompanyId { get; set; }

    public int CountryId { get; set; }

    public int StatusId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string InventoryNumber { get; set; } = null!;

    public DateOnly ManufactureDate { get; set; }

    public DateOnly CommissioningDate { get; set; }

    public DateOnly AddedToSystemDate { get; set; }

    public DateOnly? LastVerificationDate { get; set; }

    public int? VerificationIntervalMonths { get; set; }

    public DateOnly? NextVerificationDate { get; set; }

    public int ResourceHours { get; set; }

    public DateOnly? NextMaintenanceDate { get; set; }

    public int? ServiceTimeHours { get; set; }

    public int? LastVerifierUserId { get; set; }

    public DateOnly? LastInventoryDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual User? LastVerifierUser { get; set; }

    public virtual ICollection<MachineProductStock> MachineProductStocks { get; set; } = new List<MachineProductStock>();

    public virtual MachineType MachineType { get; set; } = null!;

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual Company ManufacturerCompany { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual MachineStatus Status { get; set; } = null!;

    public virtual ICollection<Verification> Verifications { get; set; } = new List<Verification>();
}
