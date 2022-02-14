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
        
        }

    }
}
