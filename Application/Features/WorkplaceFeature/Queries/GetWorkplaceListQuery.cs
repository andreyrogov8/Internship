using Application.Features.BookingFeature.Commands;
using Application.Interfaces;
using Application.Telegram;
using Application.Telegram.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.CountryCQ
{
    public class Request
    {
        public string MapId { get; set; }

        public bool? HasWindow { get; set; } = null;
        public bool? HasPc { get; set; } = null;
        public bool? HasMonitor { get; set; } = null;
        public bool? HasKeyboard { get; set; } = null;
        public bool? HasMouse { get; set; } = null;
        public bool? HasHeadset { get; set; } = null;

    }
    public class GetWorkplaceListQueryRequest : Request, IRequest <GetWorkplaceListQueryResponse>
    {

        public DateTimeOffset? StartDate { get; set; } = null;

        public DateTimeOffset? EndDate { get; set; } = null;
        
        public string RecurringDay { get; set; }
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
                DateTimeOffset startDate = query.StartDate.HasValue ? query.StartDate.Value.DateTime : DateTime.MinValue;
                DateTimeOffset endDate = query.EndDate.HasValue ? query.EndDate.Value.DateTime : DateTime.MinValue;

                if (query.RecurringDay is null)
                {
                    workplaces = GetWorkplaces(workplaces, startDate, endDate);
                }
                else 
                {
                    var bookingDays = Helper.GetRecurringDays(startDate, endDate);
                    List<IQueryable<Workplace>> temporary = new List<IQueryable<Workplace>>();
                    foreach (var item in bookingDays)
                    {
                        temporary.Add(GetWorkplaces(workplaces, item, item)); 
                    }
                    for (int i = 0; i < temporary.Count-1; i++)
                    {
                        workplaces = temporary[i].Intersect(temporary[i + 1]);
                    }
                }
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

        public IQueryable<Workplace> GetWorkplaces(IQueryable<Workplace> workplaces, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var workplacesIds = workplaces.Select(x => x.Id).Distinct();

            var interestedBookingsNonRecurring = _context.Bookings.Where(x => workplacesIds.Contains(x.WorkplaceId) && !x.IsRecurring);

            //finding busy workplaces Ids in this period non recurring
            var busyWorkplaces = interestedBookingsNonRecurring.Where(x =>
                      ((startDate > x.StartDate) && (startDate < x.EndDate))
                      || ((endDate > x.StartDate) && (endDate < x.EndDate))
                    ).Select(x => x.WorkplaceId).Distinct().ToList();

            //finding busy workplaces Id in this period recurring
            var interestedBookingsRecurring = _context.Bookings.Where(x => workplacesIds.Contains(x.WorkplaceId) && x.IsRecurring);

            foreach (var booking in interestedBookingsRecurring)
            {
                var busyDaysDate = Helper.GetRecurringDays(booking.StartDate, booking.EndDate);
                foreach (var date in busyDaysDate)
                {
                    if (startDate <= date && date <= endDate)
                    {
                        busyWorkplaces.Add(booking.WorkplaceId);
                        break;
                    }
                }
            }

            workplaces = workplaces.Where(x => !busyWorkplaces.Contains(x.Id));
            return workplaces;
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

