using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookingFeature.Commands
{

    public class DeleteBookingCommandRequest : IRequest<DeleteBookingCommandResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommandRequest, DeleteBookingCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DeleteBookingCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<DeleteBookingCommandResponse> Handle(DeleteBookingCommandRequest request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(booking => booking.Id == request.Id, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException(nameof(booking), request.Id);
            }
            booking.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteBookingCommandResponse
            {
                Id = booking.Id
            };
        }
    }

    public class DeleteBookingCommandResponse
    {
        public int Id { get; set; }
    }
}
