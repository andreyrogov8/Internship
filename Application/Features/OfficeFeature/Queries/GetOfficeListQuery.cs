using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OfficeFeature.Queries
{
    public class GetOfficeListQueryRequest : IRequest<GetOfficeListQueryResponse>
    {
    }
    public class GetOfficeListQueryHandler : IRequestHandler<GetOfficeListQueryRequest, GetOfficeListQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOfficeListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetOfficeListQueryResponse> Handle(GetOfficeListQueryRequest query, CancellationToken cancellationToken)
        {
            var offices = await _context.Offices
                .ProjectTo<OfficeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetOfficeListQueryResponse
            {
                Results = offices
            };

        }
    }

    public class GetOfficeListQueryResponse
    {
        public IEnumerable<OfficeDto> Results { get; set; }
    }
    public class OfficeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool HasFreeParking { get; set; }
    }
}
