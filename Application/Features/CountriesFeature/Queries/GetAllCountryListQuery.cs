using Application.DTO;
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
    public class GetAllCountryListQueryRequest : IRequest <IEnumerable<Responce>>
    {
    }

    public class GetAllCountryListQueryHandler : IRequestHandler<GetAllCountryListQueryRequest, IEnumerable<Responce>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCountryListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Responce> Handle(GetAllCountryListQueryRequest query, CancellationToken cancellationToken)
        {
            var countryList = await _context.Country.ToListAsync(cancellationToken);

            var response = _mapper.Map<Responce>(countryList);
            return response;
        }
    }

    public class Responce
    {
        public IEnumerable<CountryDto> Countries { get; set; }
    }
    public class CountryDto 
    {
        public string Name { get; set; }
    }
}
