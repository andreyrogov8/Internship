using Application.Features.VacationFeature.Commands;
using Application.Features.VacationFeature.Queries;
using AutoMapper;
using Domain.Models;

namespace Application.Features.VacationFeature
{
    public class VacationProfile : Profile
    {
        public VacationProfile()
        {
            CreateMap<Vacation, VacationDTO>();
            CreateMap<Vacation, GetVacationByIdQueryResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(source => source.User.UserName));

            CreateMap<CreateVacationCommandRequest, Vacation>();
            CreateMap<UpdateVacationCommandRequest, Vacation>();
        }
    }
}
