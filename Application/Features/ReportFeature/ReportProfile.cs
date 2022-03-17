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
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Workplace.Map.Office.City))
                .ForMember(dest => dest.Office, opt => opt.MapFrom(src => src.Workplace.Map.Office.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Workplace.Map.Office.Country))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Workplace.Map.Office.Address));

            CreateMap<User, UserDto>();
        }
    }
}
