using Application.Dtos;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface ISensorService
    {
        Task<List<DeviceDto>> GetDevicesAsync(DeviceStatus deviceStatus = DeviceStatus.ACTIVE);
        Task<bool> UpsertDeviceAsync(DeviceDto deviceDto);

        Task<bool> CreateDeviceReadingAsync(DeviceReadingDto deviceReadingDto);
        Task<List<DeviceReadingDto>> GetDeviceReadingsAsync(DeviceReadingFilterCriteria criteria);

        Task<List<AuditLogDto>> GetAuditLogsAsync(int deviceId, int flag = -1);

        Task<DeviceMetrics> CalculateMetricsAsync(int deviceId, int lastNHours = 1);
        Task<DeviceStatistics> CalculateStatsAsync(int deviceId);
    }
}
