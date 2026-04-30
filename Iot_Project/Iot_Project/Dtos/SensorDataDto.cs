namespace Iot_Project.Dtos
{
    public class SensorDataMinMax
    {
        public MinMaxValue Temperature { get; set; }
        public MinMaxValue Humidity { get; set; }
        public MinMaxValue Pm2_5 { get; set; }
        public MinMaxValue Pm10 { get; set; }
        public MinMaxValue Pm1_0 { get; set; }
        public MinMaxValue Co2 { get; set; }
    }
    public class MinMaxValue
    {
        public double Min { get; set; }
        public double Max { get; set; }
    }

    public class SensorDataDto
    {
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Co2 { get; set; }
        public int? PM1 { get; set; }
        public int? PM25 { get; set; }
        public int? PM10 { get; set; }
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
        public DateTime? CreatedAt { get; set; }
    }
}