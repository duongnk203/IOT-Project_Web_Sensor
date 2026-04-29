using Iot_Project.Dtos;
using Iot_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
