using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookingFeature.Queries
{
    public class GetBookingListQueryRequest : IRequest<GetBookingListQueryResponse>
    {
        public long? TelegramId { get; set; }   
    }

    public class GetBookingListQueryHandler : IRequestHandler<GetBookingListQueryRequest, GetBookingListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetBookingListQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetBookingListQueryResponse> Handle(GetBookingListQueryRequest request, CancellationToken cancellationToken)
        {
            var bookingList = _context.Bookings.AsQueryable();
            if (request.TelegramId.HasValue)
            {
                bookingList = bookingList
                    .Where(book => book.User.TelegramId == request.TelegramId);
            }
            return new GetBookingListQueryResponse
            {
                Results = await bookingList
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }
    }


    public class GetBookingListQueryResponse
    {
        public List<BookingDto> Results { get; set; }
    }
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public int Frequency { get; set; }
        public int WorkplaceId { get; set; }

        public int WorkplaceNumber { get; set; }
        public int FloorNumber { get; set; }
        public string OfficeName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool HasWindow { get; set; }
        public bool HasPc { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
    }

}
