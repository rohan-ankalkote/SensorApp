using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeviceReadingDto, DeviceReading>().ReverseMap();
            CreateMap<ThresholdAlertDto, ThresholdAlert>().ReverseMap();
            CreateMap<AuditLogDto, AuditLog>().ReverseMap();
            CreateMap<Device, DeviceDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit.ToString()));
            CreateMap<DeviceDto, Device>()
                .ForMember(d => d.Type, o => o.MapFrom(s => Enum.Parse<DeviceType>(s.Type)))
                .ForMember(d => d.Status, o => o.MapFrom(s => Enum.Parse<DeviceStatus>(s.Status)))
                .ForMember(d => d.Unit, o => o.MapFrom(s => Enum.Parse<Unit>(s.Unit)))
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore());
        }
    }
}
