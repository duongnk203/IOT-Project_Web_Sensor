using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class Device
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Location { get; set; }

    public DateTime? LastActive { get; set; }

    public virtual ICollection<DeviceCommand> DeviceCommands { get; set; } = new List<DeviceCommand>();

    public virtual ICollection<DeviceConfig> DeviceConfigs { get; set; } = new List<DeviceConfig>();

    public virtual ICollection<SensorDatum> SensorData { get; set; } = new List<SensorDatum>();
}
