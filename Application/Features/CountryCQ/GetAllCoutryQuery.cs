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
    public class GetAllCoutryQuery : IRequest <IEnumerable<CountryDto>>
    {
    }

    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCoutryQuery, IEnumerable<CountryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CountryDto>> Handle(GetAllCoutryQuery query, CancellationToken cancellationToken)
        {
            var countryList = await _context.Country.ToListAsync();
            var response = _mapper.Map<IEnumerable<CountryDto>>(countryList);
            return response;
        }
    }
}
