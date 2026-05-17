using Domain.Enums;

namespace Application.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Threshold { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int Interval { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<string> Errors { get; set; } = [];
    }

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

    public class ThresholdAlertDto
    {
        public int Id { get; set; }
        public int DeviceReadingId { get; set; }
        public int Value { get; set; }
        public int Flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class DeviceReadingFilterCriteria
    {
        public DeviceType? DeviceType { get; set; }
        public int? DeviceId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
