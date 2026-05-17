using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AuditLogRepository(SensorContext sensorContext) : IAuditLogRepository
    {
        public async Task<List<AuditLog>> GetLogsAsync(int deviceId, int flag)
        {
            var entities = await sensorContext.AuditLogs.Where(l => l.DeviceId == deviceId && l.Flag == flag).ToListAsync();

            return entities;
        }
    }
}
