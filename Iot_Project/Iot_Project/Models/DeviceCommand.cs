using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class DeviceCommand
{
    public int Id { get; set; }

    public string? DeviceId { get; set; }

    public string? Command { get; set; }

    public DateTime? CreatedAt { get; set; }
}
