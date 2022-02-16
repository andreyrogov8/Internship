using Application.Exceptions;
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
    public class GetWorkplaceByIdQueryRequest : IRequest <GetWorkplaceByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetWorkplaceByIdQueryHandler : IRequestHandler<GetWorkplaceByIdQueryRequest, GetWorkplaceByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetWorkplaceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetWorkplaceByIdQueryResponse> Handle(GetWorkplaceByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var workplace = await _context.Workplaces
                .ProjectTo<GetWorkplaceByIdQueryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (workplace == null)
            {
                throw new NotFoundException(nameof(workplace), request.Id);
            }

            return workplace;
        }
    }

    public class GetWorkplaceByIdQueryResponse
    {
        public int Id { get; set; }
        public int WorkplaceNumber { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
    }

}
