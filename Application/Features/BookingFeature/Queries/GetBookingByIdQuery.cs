using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookingFeature.Queries
{
    public class GetBookingByIdQueryRequest : IRequest<GetBookingByIdQueryResponse>
    {
        public int Id { get; set; }
    }
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQueryRequest, GetBookingByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetBookingByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetBookingByIdQueryResponse> Handle(GetBookingByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .ProjectTo<GetBookingByIdQueryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (booking == null)
            {
                throw new NotFoundException(nameof(booking), request.Id);
            }
            return booking;
        }
    }


    public class GetBookingByIdQueryResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public int Frequency { get; set; }
        public int WorkplaceId { get; set; }
        public string UserName { get; set; }
        
    }
}
