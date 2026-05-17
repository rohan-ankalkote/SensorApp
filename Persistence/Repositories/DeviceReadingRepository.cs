using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DeviceReadingRepository(SensorContext sensorContext) : IDeviceReadingRepository
    {
        public async Task<List<DeviceReading>> GetDeviceReadingsAsync(DeviceType? deviceType = null, int? deviceId = null, DateTime? from = null, DateTime? to = null)
        {
            var query = sensorContext.DeviceReadings.AsQueryable();

            if(deviceType is not null)
            {
                query = query.Where(dr => dr.Device!.Type == deviceType);
            }

            if(deviceId is not null)
            {
                query = query.Where(dr => dr.DeviceId == deviceId);
            }

            if(from is not null)
            {
                query = query.Where(dr => dr.ReadingTime >= from);
            }

            if (to is not null)
            {
                query = query.Where(dr => dr.ReadingTime <= to);
            }

            query = query.OrderByDescending(dr => dr.ReadingTime);

            var results = await query.Include(dr => dr.ThresholdAlerts).ToListAsync();

            return results;
        }

        public async Task<bool> InsertAsync(DeviceReading entity)
        {
            entity.CreatedAt = DateTime.Now;

            sensorContext.DeviceReadings.Add(entity);
            sensorContext.AuditLogs.Add(new()
            {
                DeviceId = entity.DeviceId,
                Flag = 0,
                Message = "data saved",
                CreatedAt = DateTime.Now,
            });

            var device = await sensorContext.Devices.FindAsync(entity.DeviceId);

            if(device is not null && entity.PrimaryValue > device.Threshold)
            {
                entity.ThresholdAlerts = 
                [
                    new() 
                    {
                        Flag = 1,
                        Message = "AUTO ALERT",
                        Value = entity.PrimaryValue,
                        CreatedAt = DateTime.Now,
                    }
                ];
                sensorContext.AuditLogs.Add(new()
                {
                    DeviceId = entity.DeviceId,
                    Flag = 1,
                    Message = $"alert val={entity.PrimaryValue}",
                    CreatedAt = DateTime.Now,
                });
            }

            await sensorContext.SaveChangesAsync();

            return true;
        }
    }
}
