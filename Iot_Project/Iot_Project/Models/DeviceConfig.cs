using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class DeviceConfig
{
    public int Id { get; set; }

    public string? DeviceId { get; set; }

    public int? ThresholdPm25 { get; set; }

    public double? ThresholdHum { get; set; }

    public int? Speed { get; set; }

    public double? Kp { get; set; }

    public double? Ki { get; set; }

    public double? Kd { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
