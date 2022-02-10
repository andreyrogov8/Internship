using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class WorkplacesProfile : Profile
    {
        public WorkplacesProfile()
        {
            CreateMap<Workplace, WorkplaceDto>();
            CreateMap<Workplace, GetWorkplaceByIdQueryResponse>();
        }
    
        
    }
}
