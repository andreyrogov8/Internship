using Application.Features.BookingFeature.Commands;
using Application.Features.BookingFeature.Queries;
using AutoMapper;
using Domain.Models;

namespace Application.Features.BookingFeature
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Booking, GetBookingByIdQueryResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<CreateBookingCommandRequest, Booking>();
            CreateMap<UpdateBookingCommandRequest, Booking>();
            CreateMap<Booking, UpdateBookingCommandResponse>();

            CreateMap<Booking, CreateBookingCommandResponse>()
                .ForMember(dest => dest.WorkplaceNumber, opt => opt.MapFrom(src => src.Workplace.WorkplaceNumber))
                .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Workplace.Map.Office.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Workplace.Map.Office.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Workplace.Map.Office.City))
                .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.Workplace.Map.FloorNumber))
                .ForMember(dest => dest.HasWindow, opt => opt.MapFrom(src => src.Workplace.NextToWindow))
                .ForMember(dest => dest.HasPc, opt => opt.MapFrom(src => src.Workplace.HasPC))
                .ForMember(dest => dest.HasMonitor, opt => opt.MapFrom(src => src.Workplace.HasMonitor))
                .ForMember(dest => dest.HasKeyboard, opt => opt.MapFrom(src => src.Workplace.HasKeyboard))
                .ForMember(dest => dest.HasMouse, opt => opt.MapFrom(src => src.Workplace.HasMouse))
                .ForMember(dest => dest.HasHeadset, opt => opt.MapFrom(src => src.Workplace.HasHeadset))
                ;

        }

    }
}
