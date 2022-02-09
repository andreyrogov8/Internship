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
    public class GetAllWorkplaceListQueryRequest : IRequest <GetAllWorkplaceListQueryResponse>
    {
    }

    public class GetAllWorkplaceListQueryHandler : IRequestHandler<GetAllWorkplaceListQueryRequest, GetAllWorkplaceListQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllWorkplaceListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetAllWorkplaceListQueryResponse> Handle(GetAllWorkplaceListQueryRequest query, CancellationToken cancellationToken)
        {
            var workplaces = await _context.Workplaces
                .ProjectTo<WorkplaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllWorkplaceListQueryResponse
            { 
                Results = workplaces
            };  
            
        }
    }

    public class GetAllWorkplaceListQueryResponse
    {
        public IEnumerable<WorkplaceDto> Results { get; set; }
    }
    public class WorkplaceDto
    {
        public int WorkplaceNumber { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
    }
}
