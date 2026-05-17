namespace Domain.ValueObjects
{
    public class DeviceStatistics
    {
        public int Total { get; set; }
        public double Average { get; set; }
        public double Maximum { get; set; }
        public double Minimum { get; set; }
        public int Alerts { get; set; }
        public int Readings { get; set; }
        public DateTime LastReadingTime {  get; set; }
    }
}
