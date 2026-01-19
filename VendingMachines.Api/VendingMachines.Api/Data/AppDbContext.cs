using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VendingMachines.Api.Models;

namespace VendingMachines.Api.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<MachineProductStock> MachineProductStocks { get; set; }

    public virtual DbSet<MachineStatus> MachineStatuses { get; set; }

    public virtual DbSet<MachineType> MachineTypes { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasIndex(e => new { e.CompanyId, e.BrandName }, "UQ_Brands").IsUnique();

            entity.Property(e => e.BrandName).HasMaxLength(200);

            entity.HasOne(d => d.Company).WithMany(p => p.Brands)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brands_Companies");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e => e.CompanyName, "UQ_Companies_Name").IsUnique();

            entity.Property(e => e.CompanyName).HasMaxLength(200);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(e => e.CountryName, "UQ_Countries_Name").IsUnique();

            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_User");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_VM");
        });

        modelBuilder.Entity<MachineProductStock>(entity =>
        {
            entity.HasKey(e => new { e.VendingMachineId, e.ProductId });

            entity.ToTable("MachineProductStock");

            entity.HasOne(d => d.Product).WithMany(p => p.MachineProductStocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MPS_Product");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.MachineProductStocks)
                .HasForeignKey(d => d.VendingMachineId)
                .HasConstraintName("FK_MPS_VM");
        });

        modelBuilder.Entity<MachineStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.HasIndex(e => e.StatusName, "UQ_MachineStatuses_Name").IsUnique();

            entity.Property(e => e.StatusName).HasMaxLength(80);
        });

        modelBuilder.Entity<MachineType>(entity =>
        {
            entity.HasIndex(e => e.TypeName, "UQ_MachineTypes_Name").IsUnique();

            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.ToTable("Maintenance");

            entity.Property(e => e.Problems).HasMaxLength(800);
            entity.Property(e => e.WorkDescription).HasMaxLength(800);

            entity.HasOne(d => d.ExecutorUser).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.ExecutorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maintenance_Executor");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maintenance_VM");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasIndex(e => new { e.BrandId, e.ModelName }, "UQ_Models").IsUnique();

            entity.Property(e => e.ModelName).HasMaxLength(200);

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Models_Brands");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasIndex(e => e.MethodName, "UQ_PaymentMethods_Name").IsUnique();

            entity.Property(e => e.MethodName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ProductName, "UQ_Products_Name").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductDescription).HasMaxLength(500);
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.SalesTendencyPerDay).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.RoleName, "UQ_Roles_Name").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(80);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.SaleAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaleDateTime)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Sales_SaleDateTime");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_PaymentMethod");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Product");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Sales)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_VM");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ_Users_Phone").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasIndex(e => e.InventoryNumber, "UQ_VM_InventoryNumber").IsUnique();

            entity.HasIndex(e => e.SerialNumber, "UQ_VM_SerialNumber").IsUnique();

            entity.Property(e => e.InventoryNumber).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(300);
            entity.Property(e => e.NextVerificationDate).HasComputedColumnSql("(case when [LastVerificationDate] IS NULL OR [VerificationIntervalMonths] IS NULL then NULL else dateadd(month,[VerificationIntervalMonths],[LastVerificationDate]) end)", true);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VM_Country");

            entity.HasOne(d => d.LastVerifierUser).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.LastVerifierUserId)
                .HasConstraintName("FK_VM_LastVerifierUser");

            entity.HasOne(d => d.MachineType).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.MachineTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VM_MachineType");

            entity.HasOne(d => d.ManufacturerCompany).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.ManufacturerCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VM_Manufacturer");

            entity.HasOne(d => d.Model).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VM_Model");

            entity.HasOne(d => d.Status).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VM_Status");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Verifications_User");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Verifications_VM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
