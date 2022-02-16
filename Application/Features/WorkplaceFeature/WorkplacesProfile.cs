using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;
using Application.Features.WorkplaceFeature.Commands;
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
            CreateMap<Workplace, UpdateWorkplaceCommandResponse>();
            CreateMap<UpdateWorkplaceCommandRequest, Workplace>();
            CreateMap<CreateWorkplaceCommandRequest, Workplace> ();
        }
    
        
    }
}
