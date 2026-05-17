using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DeviceRepository(SensorContext sensorContext) : IDeviceRepository
    {
        public async Task<List<Device>> GetDevicesAsync(DeviceStatus deviceStatus)
        {
            var devices = await sensorContext.Devices.Where(d => d.Status == deviceStatus).ToListAsync();

            return devices;
        }

        public async Task<bool> UpsertAsync(Device device)
        {
            string? message;
            if (device.Id == 0)
            {
                device.CreatedAt = DateTime.Now;
                device.UpdatedAt = DateTime.Now;
                message = $"dev added {device.Name}";
            }
            else
            {
                device.UpdatedAt = DateTime.Now;
                message = "dev updated";
            }

            device.AuditLogs =
            [
                new()
                {
                    Flag = 0,
                    Message = message,
                    CreatedAt = DateTime.Now,
                }
            ];

            sensorContext.Devices.Update(device);

            await sensorContext.SaveChangesAsync();

            return true;
        }
    }
}
