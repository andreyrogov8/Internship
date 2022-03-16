using Application.Features.ReportFeature.Queries;
using AutoMapper;
using Domain.Models;

namespace Application.Features.ReportFeature
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.MapId, opt => opt.MapFrom(src => src.Workplace.MapId))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Workplace.Map.Office.Name));
            
            CreateMap<User, UserDto>();
        }
    }
}
