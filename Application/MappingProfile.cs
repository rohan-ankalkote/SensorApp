using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeviceReadingDto, DeviceReading>().ReverseMap();
            CreateMap<ThresholdAlertDto, ThresholdAlert>().ReverseMap();
        }
    }
}
