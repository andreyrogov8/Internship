using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Features.BookingFeature.Commands
{
    public class CreateBookingCommandRequest : IRequest<CreateBookingCommandResponse>
    {
        public int UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public int Frequency { get; set; }
        public int WorkplaceId { get; set; }
    }
        
    public class Validator : AbstractValidator<CreateBookingCommandRequest>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");
            RuleFor(x => x.WorkplaceId).NotEmpty().WithMessage("WorkplaceId must not be blank");
            RuleFor(x => x.Frequency).InclusiveBetween(1, 30).WithMessage("Frequency of booking must be range of 1 and 30");
        }
    }
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommandRequest, CreateBookingCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateBookingCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommandRequest request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request);
            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateBookingCommandResponse { Id = booking.Id };
        }
    }

    public class CreateBookingCommandResponse
    {
        public int Id { get; set; } 
    }
}
