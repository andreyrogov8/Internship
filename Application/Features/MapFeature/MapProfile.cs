using Application.Features.MapFeature.Queries;
using AutoMapper;
using Domain.Models;

namespace Application.Features.MapFeature
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Map, MapDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Office.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Office.City));
            CreateMap<Map, GetMapByIdQeryResponse>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Office.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Office.City)); 

        }
    }
}
