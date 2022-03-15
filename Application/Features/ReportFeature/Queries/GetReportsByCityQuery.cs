using Application.Features.BookingFeature.Queries;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReportFeature.Queries
{
    public class GetReportsByCityFilter
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class GetReportsByCityQueryRequest : IRequest<GetReportsByCityQueryResponse>
    {   
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }

    public class GetReportsByCityQueryHandler : IRequestHandler<GetReportsByCityQueryRequest, GetReportsByCityQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetReportsByCityQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetReportsByCityQueryResponse> Handle(GetReportsByCityQueryRequest request, CancellationToken cancellationToken)
        {
            var bookingList = _context.Bookings.AsQueryable();

            bookingList = bookingList.Where(
                booking => booking.Workplace.Map.OfficeId == request.Id 
                && booking.StartDate.Month >= request.StartDate.Month
                && booking.StartDate.Day >= request.StartDate.Day
                && booking.EndDate.Month <= request.EndDate.Month
                && booking.EndDate.Day <= request.EndDate.Day);

            return new GetReportsByCityQueryResponse
            {
                bookings = await bookingList
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }
    }
    public class GetReportsByCityQueryResponse
    {
        public IEnumerable<BookingDto> bookings { get; set; }
    }
}
