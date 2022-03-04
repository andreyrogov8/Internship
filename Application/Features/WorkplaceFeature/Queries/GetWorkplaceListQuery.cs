using Application.Features.BookingFeature.Commands;
using Application.Interfaces;
using Application.Telegram.Models;
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
    public class GetWorkplaceListQueryRequest : IRequest <GetWorkplaceListQueryResponse>
    {
        public string MapId { get; set; }
        public DateTimeOffset? StartDate { get; set; } = null;
        public DateTimeOffset? EndDate { get; set; } = null;
        public bool? HasWindow { get; set; } = null;
        public bool? HasPc { get; set; } = null;
        public bool? HasMonitor { get; set; } = null;
        public bool? HasKeyboard { get; set; } = null;
        public bool? HasMouse { get; set; } = null;
        public bool? HasHeadset { get; set; } = null;
    }

    public class GetWorkplaceListQueryHandler : IRequestHandler<GetWorkplaceListQueryRequest, GetWorkplaceListQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkplaceListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetWorkplaceListQueryResponse> Handle(GetWorkplaceListQueryRequest query, CancellationToken cancellationToken)
        {

            var workplaces = _context.Workplaces.AsQueryable();

            if (query.MapId is not null)
            {
                workplaces = workplaces.Where(x => x.MapId == Int32.Parse(query.MapId));
            }

            if (query.StartDate is not null && query.EndDate is not null)
            {
                //finding free workplaces Id in this period
                var busyWorkplacesInThisPeriod = _context.Bookings.Where(x =>                            
                          ((query.StartDate > x.StartDate) && (query.StartDate < x.EndDate))
                          || ((query.EndDate > x.StartDate) && (query.EndDate < x.EndDate))
                        ).Select(x => x.WorkplaceId).Distinct();
                workplaces = workplaces.Where(x => !busyWorkplacesInThisPeriod.Contains(x.Id));
            }

            if (query.HasWindow is not null)
            {
                workplaces = workplaces.Where(x => x.NextToWindow == query.HasWindow);
            }

            if (query.HasPc is not null)
            {
                workplaces = workplaces.Where(x => x.HasPC == query.HasPc);
            }

            if (query.HasMonitor is not null)
            {
                workplaces = workplaces.Where(x => x.HasMonitor == query.HasMonitor);
            }

            if (query.HasKeyboard is not null)
            {
                workplaces = workplaces.Where(x => x.HasKeyboard == query.HasKeyboard);
            }

            if (query.HasMouse is not null)
            {
                workplaces = workplaces.Where(x => x.HasMouse == query.HasMouse);
            }

            if (query.HasHeadset is not null)
            {
                workplaces = workplaces.Where(x => x.HasHeadset == query.HasHeadset);
            }

            return new GetWorkplaceListQueryResponse
            {
                Results = await workplaces.ProjectTo<WorkplaceDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken)
            };  
            
        }
    }

    public class GetWorkplaceListQueryResponse
    {
        public IEnumerable<WorkplaceDto> Results { get; set; }
    }
    public class WorkplaceDto
    {
        public int Id { get; set; }
        public int WorkplaceNumber { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
        
        public int MapId { get; set; } 
    }
}
