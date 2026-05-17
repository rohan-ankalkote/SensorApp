using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<List<AuditLog>> GetLogsAsync(int deviceId, int flag);
    }
}
