using Iot_Project.Dtos;
using Iot_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Iot_Project.Services
{
    public interface ISensorDataService
    {
        // Define methods for fetching and processing sensor data, e.g.:
        Task<SensorDataMinMax> GetMinMaxSensorDataAsync();
        Task<SensorDataDto> GetLatestSensorDataAsync();
        Task<List<SensorDataDto>> GetHistorySensorDataAsync();
    }
    public class SensorDataService : ISensorDataService
    {
        public _00IotProjectContext _context;
        public SensorDataService(_00IotProjectContext context)
        {
            _context = context;
        }
        public async Task<SensorDataMinMax> GetMinMaxSensorDataAsync()
        {
            var sensorData = await _context.SensorData.ToListAsync();

            return new SensorDataMinMax
            {
                Temperature = new MinMaxValue
                {
                    Min = sensorData.Where(x => x.Temperature.HasValue).Min(x => x.Temperature.Value),
                    Max = sensorData.Where(x => x.Temperature.HasValue).Max(x => x.Temperature.Value)
                },
                Humidity = new MinMaxValue
                {
                    Min = sensorData.Where(x => x.Humidity.HasValue).Min(x => x.Humidity.Value),
                    Max = sensorData.Where(x => x.Humidity.HasValue).Max(x => x.Humidity.Value)
                },
                Pm2_5 = new MinMaxValue
                {
                    Min = sensorData.Where(x => x.Pm25.HasValue).Min(x => x.Pm25.Value),
                    Max = sensorData.Where(x => x.Pm25.HasValue).Max(x => x.Pm25.Value)
                },
                Pm10 = new MinMaxValue
                {
                    Min = sensorData.Where(x => x.Pm101.HasValue).Min(x => x.Pm101.Value),
                    Max = sensorData.Where(x => x.Pm101.HasValue).Max(x => x.Pm101.Value)
                },
                Pm1_0 = new MinMaxValue
                {
                    Min = sensorData.Where(x => x.Pm10.HasValue).Min(x => x.Pm10.Value),
                    Max = sensorData.Where(x => x.Pm10.HasValue).Max(x => x.Pm10.Value)
                },
                Co2 = new MinMaxValue
                {
                    Min = sensorData.Where(x => x.Co2.HasValue).Min(x => x.Co2.Value),
                    Max = sensorData.Where(x => x.Co2.HasValue).Max(x => x.Co2.Value)
                }
            };
        }

        public Task<SensorDataDto> GetLatestSensorDataAsync()
        {
            var sensorDataLatest = _context.SensorData.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if (sensorDataLatest == null)
            {
                return Task.FromResult<SensorDataDto>(null);
            }

            var sensorDataDto = new SensorDataDto
            {
                Temperature = sensorDataLatest.Temperature,
                Humidity = sensorDataLatest.Humidity,
                Co2 = sensorDataLatest.Co2,
                Pm10 = sensorDataLatest.Pm10,
                Pm25 = sensorDataLatest.Pm25,
                CreatedAt = sensorDataLatest.CreatedAt
            };

            return Task.FromResult(sensorDataDto);
        }

        public Task<List<SensorDataDto>> GetHistorySensorDataAsync()
        {
            var sensorDataHistory = _context.SensorData.OrderByDescending(x => x.CreatedAt).Take(2).ToList();

            var sensorDataDtoList = sensorDataHistory.Select(x => new SensorDataDto
            {
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                Co2 = x.Co2,
                Pm10 = x.Pm10,
                Pm25 = x.Pm25,
                CreatedAt = x.CreatedAt
            }).ToList();

            return Task.FromResult(sensorDataDtoList);
        }
    }
}
