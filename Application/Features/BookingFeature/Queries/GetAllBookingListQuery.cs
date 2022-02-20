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
            var bookingList = new List<BookingDto>();
            if (request.TelegramId.HasValue)
            {
                bookingList = await _context.Bookings
                    .Where(book => book.User.TelegramId == request.TelegramId)
                    .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                bookingList = await _context.Bookings
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            }

            return new GetBookingListQueryResponse
            {
                Results = bookingList
            };
        }
    }


    public class GetBookingListQueryResponse
    {
        public List<BookingDto> Results { get; set; }
    }
    public class BookingDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public int Frequency { get; set; }
        public int WorkplaceId { get; set; }
    }

}
