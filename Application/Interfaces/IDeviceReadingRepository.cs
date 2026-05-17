using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface IDeviceReadingRepository
    {
        Task<bool> InsertAsync(DeviceReading entity);
        Task<List<DeviceReading>> GetDeviceReadingsAsync(DeviceType? deviceType = null, int? deviceId = null, DateTime? from = null, DateTime? to = null);
        Task<DeviceMetrics> CalcualteMetricsAsync(int deviceId, int pastNHowrs);
        Task<DeviceStatistics> CalculateStatsAsync(int deviceId);
    }
}
