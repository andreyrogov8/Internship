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
