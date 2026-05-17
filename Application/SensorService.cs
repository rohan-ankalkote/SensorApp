using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application
{
    public class SensorService(
        IMapper mapper, 
        IDeviceRepository deviceRepository, 
        IDeviceReadingRepository deviceReadingRepository, 
        IAuditLogRepository auditLogRepository) : ISensorService
    {
        public async Task<List<DeviceDto>> GetDevicesAsync(DeviceStatus deviceStatus = DeviceStatus.ACTIVE)
        {
            var devices = await deviceRepository.GetDevicesAsync(deviceStatus);

            var result = mapper.Map<List<DeviceDto>>(devices);

            return result;
        }

        public async Task<bool> UpsertDeviceAsync(DeviceDto deviceDto)
        {
            var device = mapper.Map<Device>(deviceDto);

            var upserted = await deviceRepository.UpsertAsync(device);

            return upserted;
        }

        public async Task<bool> CreateDeviceReadingAsync(DeviceReadingDto deviceReadingDto)
        {
            deviceReadingDto.ReadingTime ??= DateTime.Now;

            var entity = mapper.Map<DeviceReading>(deviceReadingDto);

            var inserted = await deviceReadingRepository.InsertAsync(entity);

            return inserted;
        }

        public async Task<List<DeviceReadingDto>> GetDeviceReadingsAsync(DeviceReadingFilterCriteria criteria)
        {
            var entities = await deviceReadingRepository.GetDeviceReadingsAsync(criteria.DeviceType, criteria.DeviceId, criteria.From, criteria.To);

            var results = mapper.Map<List<DeviceReadingDto>>(entities);

            return results;
        }

        public async Task<List<AuditLogDto>> GetAuditLogsAsync(int deviceId, int flag = -1)
        {
            var logs = await auditLogRepository.GetLogsAsync(deviceId, flag);

            var results = mapper.Map<List<AuditLogDto>>(logs);

            return results;
        }

        public async Task<DeviceMetrics> CalculateMetricsAsync(int deviceId, int lastNHours = 1)
        {
            var result = await deviceReadingRepository.CalcualteMetricsAsync(deviceId, lastNHours);

            return result;
        }

        public async Task<DeviceStatistics> CalculateStatsAsync(int deviceId)
        {
            var result = await deviceReadingRepository.CalculateStatsAsync(deviceId);

            return result;
        }
    }
}
