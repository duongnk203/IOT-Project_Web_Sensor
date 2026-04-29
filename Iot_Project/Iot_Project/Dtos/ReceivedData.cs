namespace Iot_Project.Dtos
{
    public class ReceivedData
    {
        public string DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public int PM25 { get; set; }
        public bool Smoke { get; set; }
        public bool Relay1 { get; set; }
        public bool Relay2 { get; set; }
        public bool EnvAlarm { get; set; }
        public bool DryAlarm { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
