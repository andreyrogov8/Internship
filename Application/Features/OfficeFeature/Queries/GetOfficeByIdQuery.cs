using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OfficeFeature.Queries
{
    public class GetOfficeByIdQueryRequest : IRequest<GetOfficeByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQueryRequest, GetOfficeByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetOfficeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetOfficeByIdQueryResponse> Handle(GetOfficeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices
                .ProjectTo<GetOfficeByIdQueryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (office == null)
            {
                throw new NotFoundException(nameof(office), request.Id);
            }

            return office;
        }
    }

    public class GetOfficeByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool HasFreeParking { get; set; }
    }
}
