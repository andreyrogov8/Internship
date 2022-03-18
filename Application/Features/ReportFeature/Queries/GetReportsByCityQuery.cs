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
    public class GetReportsByCityQueryRequest : IRequest<GetReportsByCityQueryResponse>
    {   
        public string City { get; set; }
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

            bookingList = bookingList.Where(booking => booking.Workplace.Map.Office.City == request.City);

            if (bookingList.Any() && request.StartDate > DateTimeOffset.MinValue && request.EndDate > DateTimeOffset.MinValue)
            {
                bookingList = bookingList.Where(b =>
                      b.StartDate.Date >= request.StartDate.Date &&
                      b.EndDate.Date <= request.EndDate.Date);
            }

            return new GetReportsByCityQueryResponse
            {
                bookings = await bookingList
                .ProjectTo<BookingDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }
    }
    public class GetReportsByCityQueryResponse
    {
        public IEnumerable<BookingDTO> bookings { get; set; }
    }
}
