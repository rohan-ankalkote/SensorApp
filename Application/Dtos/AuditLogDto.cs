namespace Application.Dtos
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Flag { get; set; }
    }
}
