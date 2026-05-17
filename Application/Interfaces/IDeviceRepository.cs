using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IDeviceRepository
    {
        Task<bool> UpsertAsync(Device device);
        Task<List<Device>> GetDevicesAsync(DeviceStatus deviceStatus);
    }
}
