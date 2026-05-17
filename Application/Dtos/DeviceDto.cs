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
}
