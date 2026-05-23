using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class DeviceCommand
{
    public int Id { get; set; }

    public string? DeviceId { get; set; }

    public string? Command { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Mode { get; set; }

    public int? Speed { get; set; }

    public int? DurationMs { get; set; }

    public bool IsProcessed { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
