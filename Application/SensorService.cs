using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application
{
    public class SensorService(IDeviceRepository deviceRepository) : ISensorService
    {
        public async Task<List<DeviceDto>> GetDevicesAsync(DeviceStatus deviceStatus = DeviceStatus.ACTIVE)
        {
            var devices = await deviceRepository.GetDevicesAsync(deviceStatus);

            var result = devices.Select(DeviceToDeviceDto).ToList();

            return result;
        }

        public async Task<bool> UpsertDeviceAsync(DeviceDto deviceDto)
        {
            var device = DeviceDtoToDevice(deviceDto);

            var upserted = await deviceRepository.UpsertAsync(device);

            return upserted;
        }


        private static DeviceDto DeviceToDeviceDto(Device device)
        {
            return new DeviceDto
            {
                Id = device.Id,
                Name = device.Name,
                Location = device.Location,
                Type = device.Type.ToString(),
                Status = device.Status.ToString(),
                Threshold = device.Threshold,
                Unit = device.Unit.ToString(),
                Interval = device.Interval,
                CreatedAt = device.CreatedAt,
                UpdatedAt = device.UpdatedAt,
            };
        }

        private static Device DeviceDtoToDevice(DeviceDto dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Location = dto.Location,
                Type = Enum.Parse<DeviceType>(dto.Type),
                Status = Enum.Parse<DeviceStatus>(dto.Status),
                Threshold = dto.Threshold,
                Unit = Enum.Parse<Unit>(dto.Unit),
                Interval = dto.Interval,
            };
        }
    }
}
