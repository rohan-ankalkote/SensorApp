namespace Application.Dtos
{
    public class ThresholdAlertDto
    {
        public int Id { get; set; }
        public int DeviceReadingId { get; set; }
        public int Value { get; set; }
        public int Flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
