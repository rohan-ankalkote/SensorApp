namespace Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Flag { get; set; }

        public Device? Device { get; set; }
    }
}
