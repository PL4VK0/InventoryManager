using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DALef.Models;

public partial class InventoryManagerContext : DbContext
{
    public InventoryManagerContext()
    {
    }

    public InventoryManagerContext(DbContextOptions<InventoryManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblInventory> TblInventories { get; set; }

    public virtual DbSet<TblManager> TblManagers { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblWare> TblWares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=YAROSLAV;Initial Catalog=InventoryManager;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblInventory>(entity =>
        {
            entity.HasKey(e => e.WareId);
            entity.ToTable("tblInventory");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.WareId).HasColumnName("wareID");
            entity.Property(e => e.WareName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("wareName");

            entity.HasOne(d => d.Ware).WithMany()
                .HasForeignKey(d => d.WareId)
                .HasConstraintName("FK_tblInventory_tblWare");
        });

        modelBuilder.Entity<TblManager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__tblManag__47E0147FF3ECD15F");

            entity.ToTable("tblManager");

            entity.Property(e => e.ManagerId).HasColumnName("managerID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasMaxLength(19)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("salt");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__tblOrder__0809337DC30FFB42");

            entity.ToTable("tblOrder");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ManagerId).HasColumnName("managerID");
            entity.Property(e => e.WareId).HasColumnName("wareID");
        });

        modelBuilder.Entity<TblWare>(entity =>
        {
            entity.HasKey(e => e.WareId).HasName("PK__tblWare__A6A6F95B867E926C");

            entity.ToTable("tblWare");

            entity.Property(e => e.WareId).HasColumnName("wareID");
            entity.Property(e => e.WareCost).HasColumnName("wareCost");
            entity.Property(e => e.WareDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("wareDescription");
            entity.Property(e => e.WareName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("wareName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
