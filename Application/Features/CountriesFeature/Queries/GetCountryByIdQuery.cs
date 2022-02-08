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
        public GetCountryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetCountryByIdQueryResponse> Handle(GetCountryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var coutry = await _context.Country
                .ProjectTo<GetCountryByIdQueryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (coutry == null)
            {
                //Todo make custom exceprion 'throw new NotFoundException();'
            }

            return coutry;
        }
    }

    public class GetCountryByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
