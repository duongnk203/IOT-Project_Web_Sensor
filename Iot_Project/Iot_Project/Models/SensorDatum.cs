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
}
