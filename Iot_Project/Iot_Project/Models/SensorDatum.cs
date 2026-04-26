using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class SensorDatum
{
    public int Id { get; set; }

    public string? DeviceId { get; set; }

    public double? Temperature { get; set; }

    public double? Humidity { get; set; }

    public int? Pm25 { get; set; }

    public bool? Smoke { get; set; }

    public bool? Relay1 { get; set; }

    public bool? Relay2 { get; set; }

    public bool? Alarm { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Device? Device { get; set; }
}
