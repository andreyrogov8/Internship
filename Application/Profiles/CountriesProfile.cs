using Application.Features.CountryCQ;
using AutoMapper;
using MediaBrowser.Model.Globalization;
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
            CreateMap<CountryList, Responce>();
        }
    
        
    }
}
