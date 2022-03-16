
using Application.Interfaces;
using Application.Telegram;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookingFeature.Commands
{
    public abstract class UpsertBookingCommand
    {
        private readonly IApplicationDbContext _context;

        public UpsertBookingCommand(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task EnsureWorkplaceIsFreeAsync(int workplaceId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var busy = await _context.Bookings.AnyAsync(x => 
                          (x.WorkplaceId == workplaceId && (x.StartDate < startDate) && (startDate < x.EndDate ))
                        ||(x.WorkplaceId == workplaceId && (x.StartDate < endDate) && (endDate < x.EndDate))
                        );

            if (busy)
            {
                throw new ValidationException($"Workingplace {workplaceId} is already booked for this period" +
                    $"{startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}");
            }
        }

        public async Task EnsureUserHasNotBookingThisTimeAsync(int userId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var hasBooking = await _context.Bookings.AnyAsync(x => 
                           (x.UserId == userId && (x.StartDate < startDate) && (startDate < x.EndDate))
                        || (x.UserId == userId && (x.StartDate < endDate) && (endDate < x.EndDate)));

            if (hasBooking)
            {
                throw new ValidationException($"User has another booking for this period" +
                    $"{startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}");
            }
        }

        public async Task EnsureUserHasNotVacationForThisPerdioAsync(int userId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var hasVacation = await _context.Vacations.AnyAsync(x =>
                           (x.UserId == userId && (x.VacationStart < startDate) && (startDate < x.VacationEnd))
                        || (x.UserId == userId && (x.VacationStart < endDate) && (endDate < x.VacationEnd)));

            if (hasVacation)
            {
                throw new ValidationException($"You are not allowed to make booking for this period: " +
                    $"{startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}, " +
                    $"because you have vacation crossed with this booking");
            }
        }
    }
}
