using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
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
    }
}
