using Application.Dtos;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface ISensorService
    {
        Task<List<DeviceDto>> GetDevicesAsync(DeviceStatus deviceStatus = DeviceStatus.ACTIVE);
        Task<bool> UpsertDeviceAsync(DeviceDto deviceDto);

        Task<bool> CreateDeviceReadingAsync(DeviceReadingDto deviceReadingDto);
        Task<List<DeviceReadingDto>> GetDeviceReadingsAsync(DeviceReadingFilterCriteria criteria);
    }
}
