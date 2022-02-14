using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommandRequest>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");
            RuleFor(x => x.WorkplaceId).NotEmpty().WithMessage("WorkplaceId must not be blank");
            RuleFor(x => x.IsRecurring).Must(x => x == false || x == true).WithMessage("IsRecurring should be whether true or false");
            RuleFor(x => x.Frequency).InclusiveBetween(1, 30).WithMessage("Frequency of booking must be range of 1 and 30");
        }
    }
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommandRequest, CreateBookingCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateBookingCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommandRequest request, CancellationToken cancellationToken)
        {

            var isUserExistsWithThisId = await _userManager.Users.AnyAsync(user => user.Id == request.UserId, cancellationToken);
            
            if (!isUserExistsWithThisId)
            {
                throw new NotFoundException($"There is no User with id={request.UserId}");
            }
            var isWorkPlaceExistsWithThisId = await _context.Workplaces.AnyAsync(w => w.Id == request.WorkplaceId, cancellationToken);

            if (!isWorkPlaceExistsWithThisId)
            {
                throw new NotFoundException($"There is no WorkPlace with id={request.WorkplaceId}");
            }
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
