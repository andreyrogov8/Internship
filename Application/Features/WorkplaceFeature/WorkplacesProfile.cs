using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;
using Application.Features.MapFeatures.Queries;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class WorkplacesProfile : Profile
    {
        public WorkplacesProfile()
        {
            CreateMap<Workplace, WorkplaceDto>();
            CreateMap<Workplace, GetWorkplaceByIdQueryResponse>();
            CreateMap<Map, MapDto>();
        }
    
        
    }
}
