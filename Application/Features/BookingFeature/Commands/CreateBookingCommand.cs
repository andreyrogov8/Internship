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
            RuleFor(x => x.UserId).NotEmpty().WithMessage("WorkplaceId must not be blank");
        }
    }
    public class CreateBookingCommandHandler : UpsertBookingCommand, IRequestHandler<CreateBookingCommandRequest, CreateBookingCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateBookingCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager) : base(context)
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

            await EnsureWorkplaceIsFreeAsync(request.WorkplaceId, request.StartDate, request.EndDate, request.IsRecurring);
            await EnsureUserHasNotBookingThisTimeAsync(request.UserId, request.StartDate, request.EndDate, request.IsRecurring);
            await EnsureUserHasNotVacationForThisPeriodAsync(request.UserId, request.StartDate, request.EndDate);          

            var booking = _mapper.Map<Booking>(request);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CreateBookingCommandResponse>(_context.Bookings.Include(x => x.Workplace.Map.Office).Where(x=>x.Id == booking.Id).FirstOrDefault());
        }
    }

    public class CreateBookingCommandResponse
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
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
