using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class DeviceConfig
{
    public int Id { get; set; }

    public string? DeviceId { get; set; }

    public double? Kp { get; set; }

    public double? Ki { get; set; }

    public double? Kd { get; set; }

    public int? Speed { get; set; }

    public int? ThresholdPm25 { get; set; }

    public double? ThresholdHumidity { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Device? Device { get; set; }
}
