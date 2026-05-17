using Domain.Enums;

namespace Application.Dtos
{
    public class DeviceReadingFilterCriteria
    {
        public DeviceType? DeviceType { get; set; }
        public int? DeviceId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
