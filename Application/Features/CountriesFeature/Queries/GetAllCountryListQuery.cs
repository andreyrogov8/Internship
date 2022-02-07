using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CountryCQ
{
    public class GetAllCountryListQueryRequest : IRequest <GetAllCountryListQueryResponse>
    {
    }

    public class GetAllCountryListQueryHandler : IRequestHandler<GetAllCountryListQueryRequest, GetAllCountryListQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCountryListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetAllCountryListQueryResponse> Handle(GetAllCountryListQueryRequest query, CancellationToken cancellationToken)
        {
            var coutry = await _context.Country
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllCountryListQueryResponse
            { 
                Results = coutry.ToList() 
            };  
            
        }
    }

    public class GetAllCountryListQueryResponse
    {
        public IEnumerable<CountryDto> Results { get; set; }
    }
    public class CountryDto 
    {
        public string Name { get; set; }
    }
}
