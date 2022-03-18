﻿using Application.Features.BookingFeature.Queries;
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

namespace Application.Features.ReportFeature.Queries
{
    public class GetReportsByFloorRequest : IRequest<GetReportsByFloorResponse>
    {
        public int FloorId { get; set; }
        public string OfficeName { get; set; }
        public int FloorNumber { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
    public class GetReportsByFloorHandler : IRequestHandler<GetReportsByFloorRequest, GetReportsByFloorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetReportsByFloorHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetReportsByFloorResponse> Handle(GetReportsByFloorRequest request, CancellationToken cancellationToken)
        {
            var bookingList = _context.Bookings.AsQueryable();

            if (request.FloorId>0 )
            {
                bookingList = bookingList.Where(b =>
                    b.Workplace.MapId == request.FloorId);
            }

            if (request.OfficeName is not null && request.FloorNumber > 0 )
            {
                bookingList = bookingList.Where(b =>
                    b.Workplace.Map.Office.Name == request.OfficeName &&
                    b.Workplace.Map.FloorNumber == request.FloorNumber);
            }

            if (bookingList.Count()>0 && request.StartDate>DateTimeOffset.MinValue && request.EndDate > DateTimeOffset.MinValue)
            {
                bookingList = bookingList.Where(b =>
                      b.StartDate >= request.StartDate &&
                      b.EndDate <= request.EndDate);
            }               

            return new GetReportsByFloorResponse
            {
                bookings = await bookingList
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }
    }
    public class GetReportsByFloorResponse
    {
        public IEnumerable<BookingDto> bookings { get; set; }
    }

}

