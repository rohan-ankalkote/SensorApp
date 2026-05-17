using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Repositories
{
    public class DeviceReadingRepository(SensorContext sensorContext, ILogger<DeviceReadingRepository> logger) : IDeviceReadingRepository
    {
        public async Task<DeviceMetrics> CalcualteMetricsAsync(int deviceId, int pastNHowrs)
        {
            var from = DateTime.Now.AddHours(-1 * pastNHowrs);
            var threshold = await sensorContext.Devices.Where(d => d.Id == deviceId).Select(d => d.Threshold).FirstOrDefaultAsync();

            var query = sensorContext.DeviceReadings.Where(r => r.ReadingTime >= from && r.DeviceId == deviceId);

            var result = query.GroupBy(k => k.DeviceId, e => e, (k, g) => new DeviceMetrics
            {
                Threshold = threshold,
                Average = g.Average(x => x.PrimaryValue),
                Maximum = g.Max(x => x.PrimaryValue)
            }).FirstOrDefault();

            if(result == null)
            {
                logger.LogWarning("No data found for device {DeviceId}", deviceId);
                throw new Exception($"No data found while calculating metrics for device {deviceId}");
            }

            sensorContext.AuditLogs.Add(new()
            {
                Flag = 0,
                Message = $"calc device id = {deviceId}",
                CreatedAt = DateTime.Now,
                DeviceId = deviceId
            });

            await sensorContext.SaveChangesAsync();

            return result;
        }

        public async Task<DeviceStatistics> CalculateStatsAsync(int deviceId)
        {
            var query = sensorContext.DeviceReadings.AsQueryable();

            var result = await query.Where(r => r.DeviceId == deviceId).GroupBy(k => k.DeviceId, e => e, (k, g) => new DeviceStatistics
            {
                Total = g.Count(),
                Maximum = g.Max(x => x.PrimaryValue),
                Minimum = g.Min(x => x.PrimaryValue),
                Average = g.Average(x => x.PrimaryValue),
                LastReadingTime = g.Max(x => x.ReadingTime),
                Alerts = g.SelectMany(x => x.ThresholdAlerts!).Count(),
                Readings = g.Count()
            }).FirstOrDefaultAsync();

            if (result == null)
            {
                logger.LogWarning("No data found for device {DeviceId}", deviceId);
                throw new Exception($"No data found while calculating statistics for device {deviceId}");
            }

            return result;
        }

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
