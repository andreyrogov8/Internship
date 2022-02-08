using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CountriesFeature.Queries
{
    public class GetCountryByIdQueryRequest : IRequest <GetCountryByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQueryRequest, GetCountryByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public async Task<GetCountryByIdQueryResponse> Handle(GetCountryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var coutry = await _context.Country
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (coutry == null)
            {
                throw new DllNotFoundException(nameof(Country));
            }

            return new GetCountryByIdQueryResponse
            {
                Result = coutry
            };
        }
    }

    public class GetCountryByIdQueryResponse
    {
        public CountryDto Result { get; set; }
    }
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
