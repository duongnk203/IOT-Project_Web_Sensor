using System;
using System.Collections.Generic;

namespace Iot_Project.Models;

public partial class SensorDatum
{
    public int Id { get; set; }

    public double? Temperature { get; set; }

    public double? Humidity { get; set; }

    public int? Pm25 { get; set; }

    public int? Pm10 { get; set; }

    public int? Pm101 { get; set; }

    public double? Co2 { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? DeviceId { get; set; }

    public bool? Smoke { get; set; }

    public bool? Relay1 { get; set; }

    public bool? Relay2 { get; set; }

    public bool? EnvAlarm { get; set; }

    public bool? DryAlarm { get; set; }

    public string? CarState { get; set; }

    public int? ThresholdPm25 { get; set; }

    public double? ThresholdHum { get; set; }

    public int? Speed { get; set; }

    public double? Kp { get; set; }

    public double? Ki { get; set; }

    public double? Kd { get; set; }
}
