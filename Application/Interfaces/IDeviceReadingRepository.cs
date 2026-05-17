using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IDeviceReadingRepository
    {
        Task<bool> InsertAsync(DeviceReading entity);
        Task<List<DeviceReading>> GetDeviceReadingsAsync(DeviceType? deviceType = null, int? deviceId = null, DateTime? from = null, DateTime? to = null);
    }
}
