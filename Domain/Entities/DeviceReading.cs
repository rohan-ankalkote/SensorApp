namespace Domain.Entities
{
    public class DeviceReading
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime ReadingTime { get; set; }
        public double PrimaryValue { get; set; }
        public double SecondaryValue { get; set; }
        public double TertiaryValue { get; set; }
        public DateTime CreatedAt { get; set; }

        public Device? Device { get; set; }
        public List<ThresholdAlert>? ThresholdAlerts { get; set; }
    }
}
