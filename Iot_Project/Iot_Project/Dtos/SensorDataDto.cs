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
        public int? Pm10 { get; set; }
        public int? Pm25 { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}