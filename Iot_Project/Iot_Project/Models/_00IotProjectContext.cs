using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Iot_Project.Models;

public partial class _00IotProjectContext : DbContext
{
    public _00IotProjectContext()
    {
    }

    public _00IotProjectContext(DbContextOptions<_00IotProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceCommand> DeviceCommands { get; set; }

    public virtual DbSet<DeviceConfig> DeviceConfigs { get; set; }

    public virtual DbSet<SensorDatum> SensorData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devices__3214EC072F92CDBD");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.LastActive).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DeviceCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceCo__3214EC076FEB0F60");

            entity.Property(e => e.Command).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.IsExecuted).HasDefaultValue(false);

            entity.HasOne(d => d.Device).WithMany(p => p.DeviceCommands)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK__DeviceCom__Devic__4316F928");
        });

        modelBuilder.Entity<DeviceConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceCo__3214EC07B5D98B94");

            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.ThresholdPm25).HasColumnName("ThresholdPM25");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Device).WithMany(p => p.DeviceConfigs)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK__DeviceCon__Devic__3E52440B");
        });

        modelBuilder.Entity<SensorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SensorDa__3214EC0704B1A8F7");

            entity.HasIndex(e => e.CreatedAt, "IX_SensorData_CreatedAt").IsDescending();

            entity.HasIndex(e => e.DeviceId, "IX_SensorData_DeviceId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.Pm25).HasColumnName("PM25");

            entity.HasOne(d => d.Device).WithMany(p => p.SensorData)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK__SensorDat__Devic__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
