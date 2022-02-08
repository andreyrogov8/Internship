using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class CountriesProfile : Profile
    {
        public CountriesProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<Country, GetCountryByIdQueryResponse>();
        }
    
        
    }
}
