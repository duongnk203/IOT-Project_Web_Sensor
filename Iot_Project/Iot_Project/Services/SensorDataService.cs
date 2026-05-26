using Iot_Project.Dtos;
using Iot_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Iot_Project.Services
{
    public interface ISensorDataService
    {
        Task<SensorDataMinMax> GetMinMaxSensorDataAsync();
        Task<SensorDataDto> GetLatestSensorDataAsync();
        Task<List<SensorDataDto>> GetHistorySensorDataAsync();
        Task SaveSensorDataAsync(ReceivedData data);
        Task UpdateDeviceConfigAsync(DeviceConfigDto dto);
        Task CreateDeviceCommand(DeviceCommandDto dto);
        Task<DeviceConfigDto> GetDeviceConfigAsync();
        Task<DeviceCommandDto> GetLatestDeviceCommandAsync();
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
                PM10 = sensorDataLatest.Pm10,
                PM25 = sensorDataLatest.Pm25,
                Smoke = sensorDataLatest.Smoke,
                Relay1 = sensorDataLatest.Relay1,
                Relay2 = sensorDataLatest.Relay2,
                EnvAlarm = sensorDataLatest.EnvAlarm,
                DryAlarm = sensorDataLatest.DryAlarm,
                CarState = sensorDataLatest.CarState,
                ThresholdHum = sensorDataLatest.ThresholdHum,
                ThresholdPm25 = sensorDataLatest.ThresholdPm25,
                Speed = sensorDataLatest.Speed,
                Kp = sensorDataLatest.Kp,
                Ki = sensorDataLatest.Ki,
                Kd = sensorDataLatest.Kd,
                CreatedAt = sensorDataLatest.CreatedAt
            };

            return Task.FromResult(sensorDataDto);
        }

        public Task<List<SensorDataDto>> GetHistorySensorDataAsync()
        {
            var sensorDataHistory = _context.SensorData.OrderByDescending(x => x.CreatedAt).Take(10).ToList();

            var sensorDataDtoList = sensorDataHistory.Select(x => new SensorDataDto
            {
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                Co2 = x.Co2,
                PM1 = x.Pm10,
                PM25 = x.Pm25,
                PM10 = x.Pm10,
                Smoke = x.Smoke,
                Relay1 = x.Relay1,
                Relay2 = x.Relay2,
                EnvAlarm = x.EnvAlarm,
                DryAlarm = x.DryAlarm,
                CarState = x.CarState,
                ThresholdHum = x.ThresholdHum,
                ThresholdPm25 = x.ThresholdPm25,
                Speed = x.Speed,
                Kp = x.Kp,
                Ki = x.Ki,
                Kd = x.Kd,
                CreatedAt = x.CreatedAt
            }).ToList();

            return Task.FromResult(sensorDataDtoList);
        }

        public async Task SaveSensorDataAsync(ReceivedData data)
        {
            var sensorData = new SensorDatum
            {
                Temperature = data.Temperature,
                Humidity = data.Humidity,
                Co2 = 0,
                Pm101 = data.PM1,
                Pm10 = data.PM10,
                Pm25 = data.PM25,
                DeviceId = data.DeviceId,
                Smoke = data.Smoke,
                Relay1 = data.Relay1,
                Relay2 = data.Relay2,
                EnvAlarm = data.EnvAlarm,
                DryAlarm = data.DryAlarm,
                CarState = data.CarState,
                ThresholdPm25 = data.ThresholdPm25,
                ThresholdHum = data.ThresholdHum,
                Speed = data.Speed,
                Kp = data.Kp,
                Ki = data.Ki,
                Kd = data.Kd
            };

            _context.SensorData.Add(sensorData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceConfigAsync(DeviceConfigDto dto)
        {
            var latestData = await _context.DeviceConfigs.OrderByDescending(x => x.UpdatedAt).FirstOrDefaultAsync();
            if (latestData != null)
            {
                latestData.ThresholdPm25 = dto.ThresholdPm25;
                latestData.ThresholdHum = dto.ThresholdHum;
                latestData.Speed = dto.Speed;
                latestData.Kp = dto.Kp;
                latestData.Ki = dto.Ki;
                latestData.Kd = dto.Kd;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateDeviceCommand(DeviceCommandDto dto)
        {
            var now = DateTime.Now;
            var command = new DeviceCommand
            {
                DeviceId = string.IsNullOrWhiteSpace(dto.DeviceId) ? "car_001" : dto.DeviceId,
                // Bỏ ép default "AUTO"/"STOP" để tránh ghi đè logic latching của ESP
                Mode = string.IsNullOrWhiteSpace(dto.Mode) ? null : dto.Mode.ToUpperInvariant(),
                Command = string.IsNullOrWhiteSpace(dto.Command) ? null : dto.Command.ToUpperInvariant(),
                Speed = dto.Speed,
                DurationMs = dto.DurationMs,
                IsProcessed = false,
                ProcessedAt = null,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.DeviceCommands.Add(command);
            await _context.SaveChangesAsync();
        }

        public async Task<DeviceConfigDto> GetDeviceConfigAsync()
        {
            var deviceConfig = await _context.DeviceConfigs.OrderByDescending(x => x.UpdatedAt).Select(x => new DeviceConfigDto
            {
                ThresholdPm25 = x.ThresholdPm25,
                ThresholdHum = x.ThresholdHum,
                Speed = x.Speed,
                Kp = x.Kp,
                Ki = x.Ki,
                Kd = x.Kd
            }).FirstOrDefaultAsync();
            return deviceConfig;
        }

        public async Task<DeviceCommandDto> GetLatestDeviceCommandAsync()
        {
            var deviceId = "car_001";
            var latestCommand = await _context.DeviceCommands
                .Where(x => x.DeviceId == deviceId && !x.IsProcessed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (latestCommand == null)
            {
                return null;
            }

            latestCommand.IsProcessed = true;
            latestCommand.ProcessedAt = DateTime.Now;
            latestCommand.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return new DeviceCommandDto
            {
                DeviceId = latestCommand.DeviceId,
                Mode = latestCommand.Mode,
                Command = latestCommand.Command,
                Speed = latestCommand.Speed,
                DurationMs = latestCommand.DurationMs,
                CreatedAt = latestCommand.CreatedAt
            };
        }
    }
}
