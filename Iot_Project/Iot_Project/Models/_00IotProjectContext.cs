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
        modelBuilder.Entity<DeviceCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceCo__3214EC071123AAA9");

            entity.ToTable("DeviceCommand");

            entity.Property(e => e.Command)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DeviceId).HasMaxLength(500);
        });

        modelBuilder.Entity<DeviceConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceCo__3214EC07142A05D3");

            entity.ToTable("DeviceConfig");

            entity.Property(e => e.DeviceId).HasMaxLength(500);
            entity.Property(e => e.ThresholdHum).HasColumnName("ThresholdHum ");
            entity.Property(e => e.ThresholdPm25).HasColumnName("ThresholdPM25 ");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<SensorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SensorDa__3214EC0704B1A8F7");

            entity.HasIndex(e => e.CreatedAt, "IX_SensorData_CreatedAt").IsDescending();

            entity.Property(e => e.CarState)
                .HasMaxLength(500)
                .HasColumnName("CarState ");
            entity.Property(e => e.Co2).HasColumnName("CO2");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceId).HasMaxLength(500);
            entity.Property(e => e.DryAlarm).HasColumnName("DryAlarm ");
            entity.Property(e => e.EnvAlarm).HasColumnName("EnvAlarm ");
            entity.Property(e => e.Pm10).HasColumnName("PM1_0");
            entity.Property(e => e.Pm101).HasColumnName("PM10");
            entity.Property(e => e.Pm25).HasColumnName("PM2_5");
            entity.Property(e => e.Relay1).HasColumnName("Relay_1");
            entity.Property(e => e.Relay2).HasColumnName("Relay_2");
            entity.Property(e => e.ThresholdHum).HasColumnName("ThresholdHum ");
            entity.Property(e => e.ThresholdPm25).HasColumnName("ThresholdPM25 ");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
