
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Application.Features.BookingFeature.Commands
{
    public class UpdateBookingCommandRequest : IRequest<UpdateBookingCommandResponse>
    {
        public int UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public int Frequency { get; set; }
        public int WorkplaceId { get; set; }

    }

    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommandRequest>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");
            RuleFor(x => x.WorkplaceId).NotEmpty().WithMessage("WorkplaceId must not be blank");
            RuleFor(x => x.IsRecurring).Must(x => x == false || x == true).WithMessage("IsRecurring should be whether true or false");
            RuleFor(x => x.Frequency).InclusiveBetween(1, 30).WithMessage("Frequency of booking must be range of 1 and 30");
        }
    }

    public class UpdateBookingCommandHandler : UpsertBookingCommand, IRequestHandler<UpdateBookingCommandRequest, UpdateBookingCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UpdateBookingCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<UpdateBookingCommandResponse> Handle(UpdateBookingCommandRequest request, CancellationToken cancellationToken)
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
            var booking = await _context.Bookings.FirstOrDefaultAsync(booking => booking.Id == request.UserId, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException(nameof(booking), request.UserId);
            }

            await EnsureWorkplaceIsFreeAsync(request.WorkplaceId, request.StartDate, request.EndDate, request.IsRecurring);
            await EnsureUserHasNotBookingThisTimeAsync(request.UserId, request.StartDate, request.EndDate, request.IsRecurring);
            await EnsureUserHasNotVacationForThisPeriodAsync(request.UserId, request.StartDate, request.EndDate);

            booking = _mapper.Map(request, booking);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UpdateBookingCommandResponse>(booking);

        }
    }

    public class UpdateBookingCommandResponse
    {
        public int Id { get; set; }
    }
}
