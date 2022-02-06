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
    public class GetAllCoutryListQuery : IRequest <IEnumerable<CountryDto>>
    {
    }

    public class GetAllCountryListQueryHandler : IRequestHandler<GetAllCoutryListQuery, IEnumerable<CountryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCountryListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CountryDto>> Handle(GetAllCoutryListQuery query, CancellationToken cancellationToken)
        {
            var countryList = await _context.Country.ToListAsync();
            var response = _mapper.Map<IEnumerable<CountryDto>>(countryList);
            return response;
        }
    }
}
