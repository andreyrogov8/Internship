using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CountryCQ
{
    public class GetAllCountryListQueryRequest : IRequest <GetAllCountryListQueryResponce>
    {
    }

    public class GetAllCountryListQueryHandler : IRequestHandler<GetAllCountryListQueryRequest, GetAllCountryListQueryResponce>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCountryListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetAllCountryListQueryResponce> Handle(GetAllCountryListQueryRequest query, CancellationToken cancellationToken)
        {
            var countryList = await _context.Country.ToListAsync(cancellationToken);

            var response = _mapper.Map<GetAllCountryListQueryResponce>(countryList);
            return response;
        }
    }

    public class GetAllCountryListQueryResponce
    {
        public IEnumerable<CountryDto> Results { get; set; }
    }
    public class CountryDto 
    {
        public string Name { get; set; }
    }
}
