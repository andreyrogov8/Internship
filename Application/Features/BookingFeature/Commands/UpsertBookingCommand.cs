
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookingFeature.Commands
{
    public abstract class UpsertBookingCommand
    {
        private readonly IApplicationDbContext _context;

        public UpsertBookingCommand(IApplicationDbContext context)
        {
            _context = context;
        }
        public void EnsureWorkplaceIsFree(int workplaceId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var busy = _context.Bookings.Where(x => 
                          (x.WorkplaceId == workplaceId && (x.StartDate < startDate) && (startDate < x.EndDate ))
                        ||(x.WorkplaceId == workplaceId && (x.StartDate < endDate) && (endDate < x.EndDate))
                        ).Any();

            if (busy)
            {
                throw new ValidationException($"Workingplace {workplaceId} is already booked for this period" +
                    $"{startDate} - {endDate}");
            }
        }

        public void EnsureUserHasNotBookingThisTime(long telegramId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var hasBooking = _context.Bookings.Where(x => 
                           (x.UserId == telegramId && (x.StartDate < startDate) && (startDate < x.EndDate))
                        || (x.UserId == telegramId && (x.StartDate < endDate) && (endDate < x.EndDate))).Any();

            if (hasBooking)
            {
                throw new ValidationException($"User has another booking for this period" +
                    $"{startDate} - {endDate}");
            }
        }
    }
}
