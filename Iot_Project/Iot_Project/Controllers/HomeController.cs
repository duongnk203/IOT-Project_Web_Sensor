using Iot_Project.Dtos;
using Iot_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iot_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public ISensorDataService _sensorDataService;
        public HomeController(ISensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the IoT Project API!");
        }

        [HttpGet("latest")]
        public IActionResult GetLatestData()
        {
            var sensorDataLatest = _sensorDataService.GetLatestSensorDataAsync();
            return Ok(sensorDataLatest);
        }

        [HttpGet("MinMax")]
        public async Task<IActionResult> GetData()
        {
            var minMaxData = await _sensorDataService.GetMinMaxSensorDataAsync();
            return Ok(minMaxData);
        }

        [HttpGet("History")]
        public IActionResult GetHistoryData()
        {
            var sensorDataHistory = _sensorDataService.GetHistorySensorDataAsync();
            return Ok(sensorDataHistory);
        }

        [HttpPost("sensor-data")]
        public async Task<IActionResult> PostSensorData([FromBody] ReceivedData data)
        {
            if (data == null)
            {
                return BadRequest("Invalid sensor data.");
            }
            await _sensorDataService.SaveSensorDataAsync(data);
            return Ok("Sensor data received and saved successfully.");
        }

        [HttpGet("device-config")]
        public async Task<IActionResult> GetConfig()
        {
            var config = await _sensorDataService.GetDeviceConfigAsync();
            return Ok(config);
        }

        [HttpPost("device-config")]
        public async Task<IActionResult> SaveConfig([FromBody] DeviceConfigDto dto)
        {
            await _sensorDataService.UpdateDeviceConfigAsync(dto);
            return Ok("Device configuration updated successfully.");
        }

        [HttpGet("device-command")] 
        public async Task<IActionResult> GetCommand()
        {
            var command = await _sensorDataService.GetLatestDeviceCommandAsync();
            if (command == null)
            {
                return NotFound("No commands found.");
            }
            return Ok(command);
        }

        [HttpPost("device-command")]
        public async Task<IActionResult> SendCommand([FromBody] DeviceCommandDto dto)
        {
            await _sensorDataService.CreateDeviceCommand(dto);
            return Ok($"Received command: {dto.Command}");
        }

    }
}
