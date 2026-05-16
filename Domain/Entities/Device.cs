namespace Domain.Entities
{
    public enum DeviceStatus
    {
        ACTIVE = 0,
        INACTIVE = 1
    }

    public enum DeviceType
    {
        TEMPRETURE = 0,
        HUMIDITY = 1,
        PRESSURE = 2
    }

    public enum Unit
    {
        CELCIUS = 0,
        KELVIN = 1,
        FARENHEIT = 2,
    }

    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DeviceType Type { get; set; }
        public DeviceStatus Status { get; set; }
        public int Threshold { get; set; }
        public Unit Unit { get; set; }
        public int Interval { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<AuditLog>? AuditLogs { get; set; }
        public List<DeviceReading>? Readings { get; set; }
    }


    public class AuditLog
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Flag { get; set; }

        public Device? Device { get; set; }
    }

    public class DeviceReading
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime ReadingTime { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public Device? Device { get; set; }
        public List<ThresholdAlert>? ThresholdAlerts { get; set; }
    }

    public class ThresholdAlert
    {
        public int Id { get; set; }
        public int DeviceReadingId { get; set; }
        public int Value { get; set; }
        public int Flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public DeviceReading? DeviceReading { get; set; }
    }
}
