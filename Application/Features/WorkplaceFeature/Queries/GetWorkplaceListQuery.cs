﻿using Application.Features.BookingFeature.Commands;
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
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
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
                var freeWorkplacesInThisPeriod= _context.Bookings.Where(x =>                            
                          (query.StartDate < x.StartDate) || (x.EndDate < query.EndDate)
                        ).Select(x => x.WorkplaceId);
                workplaces = workplaces.Where(x => freeWorkplacesInThisPeriod.Contains(x.Id));
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
