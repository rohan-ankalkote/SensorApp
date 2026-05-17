using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace SensorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController(ISensorService sensorService) : ControllerBase
    {
        [HttpGet("devices")]
        public async Task<IActionResult> GetDevicesAsync(DeviceStatus deviceStatus)
        {
            var result = await sensorService.GetDevicesAsync(deviceStatus);

            return Ok(result);
        }

        [HttpPost("device")]
        public async Task<IActionResult> UpsertDeviceAsync(DeviceDto deviceDto)
        {
            var upserted = await sensorService.UpsertDeviceAsync(deviceDto);

            if (upserted)
            {
                return Ok(new
                {
                    Message = "Device upserted."
                });
            }

            throw new Exception("Device not upserted!");
        }

        [HttpPost("device/data")]
        public async Task<IActionResult> CreateDeviceDataAsync(DeviceReadingDto deviceReadingDto)
        {
            var inserted = await sensorService.CreateDeviceReadingAsync(deviceReadingDto);

            if (inserted)
            {
                return Ok(new
                {
                    Message = "Reading inserted."
                });
            }

            throw new Exception("Reading not inserted!");
        }

        [HttpPost("device/data/getbycriteria")]
        public async Task<IActionResult> GetDeviceReadingsAsync(DeviceReadingFilterCriteria criteria)
        {
            var dtos = await sensorService.GetDeviceReadingsAsync(criteria);

            return Ok(dtos);
        }

        [HttpGet("logs/{deviceId:int}/{flag:int}")]
        public async Task<IActionResult> GetLogsAsync(int deviceId, int flag = -1)
        {
            var logs = await sensorService.GetAuditLogsAsync(deviceId, flag);

            return Ok(logs);
        }

        [HttpGet("device/calculate-metrics/{deviceId:int}/{lastNHours:int}")]
        public async Task<IActionResult> GetMetricsAsync(int deviceId, int lastNHours)
        {
            var result = await sensorService.CalculateMetricsAsync(deviceId, lastNHours);

            return Ok(result);
        }

        [HttpGet("device/calculate-stats/{deviceId:int}")]
        public async Task<IActionResult> GetStatsAsync(int deviceId)
        {
            var result = await sensorService.CalculateStatsAsync(deviceId);

            return Ok(result);
        }
    }
}
