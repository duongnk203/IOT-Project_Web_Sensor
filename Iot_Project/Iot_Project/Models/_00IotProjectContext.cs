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
        modelBuilder.Entity<SensorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SensorDa__3214EC0704B1A8F7");

            entity.HasIndex(e => e.CreatedAt, "IX_SensorData_CreatedAt").IsDescending();

            entity.Property(e => e.Co2).HasColumnName("CO2");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Pm10).HasColumnName("PM1_0");
            entity.Property(e => e.Pm101).HasColumnName("PM10");
            entity.Property(e => e.Pm25).HasColumnName("PM2_5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
