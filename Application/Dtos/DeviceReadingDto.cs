namespace Application.Dtos
{
    public class DeviceReadingDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime? ReadingTime { get; set; }
        public double PrimaryValue { get; set; }
        public double SecondaryValue { get; set; }
        public double TertiaryValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ThresholdAlertDto> ThresholdAlerts { get; set; } = [];
    }
}
