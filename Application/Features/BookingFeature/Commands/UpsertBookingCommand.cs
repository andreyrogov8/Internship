
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
        public async Task EnsureWorkplaceIsFreeAsync(int workplaceId, DateTimeOffset startDate, DateTimeOffset endDate, bool isRecurringBooking)
        {
            var isBusy = false;
            if (isRecurringBooking)
            {
                var currentBookingRecurringDays = Helper.GetRecurringDays(startDate, endDate);
                foreach (var recurrinDayDate in currentBookingRecurringDays)
                {
                    //checkin not recurring bookings
                    isBusy = await _context.Bookings.AnyAsync(x => !x.IsRecurring &&
                                   x.WorkplaceId == workplaceId && (x.StartDate <= recurrinDayDate) && (recurrinDayDate <= x.EndDate));
                    //checkin recurring bookings
                    var thisWorkplaceInRecurringBookings = _context.Bookings.Where(x => x.IsRecurring &&
                       x.WorkplaceId == workplaceId && (x.StartDate <= recurrinDayDate) && (recurrinDayDate <= x.EndDate))
                       .Select(x => x.StartDate).ToList();

                    foreach (var item in thisWorkplaceInRecurringBookings)
                    {
                        var date = item;
                        while (date <= recurrinDayDate)
                        {
                            if (date == recurrinDayDate)
                            {
                                isBusy = true;
                                break;
                            }
                            date = date.AddDays(7);
                        }
                        if (isBusy) break;
                    }
                    if (isBusy) break;
                }
            }
            else
            {
                //checking in not recurring bookings
                isBusy = await _context.Bookings.AnyAsync(x => !x.IsRecurring &&
                               ((x.WorkplaceId == workplaceId && (x.StartDate < startDate) && (startDate < x.EndDate))
                            || (x.WorkplaceId == workplaceId && (x.StartDate < endDate) && (endDate < x.EndDate))));
                if (!isBusy)
                {
                    //checking in recurring bookings
                    var thisWorkplaceInRecurringBooking = _context.Bookings.Where(x => x.IsRecurring &&
                       ((x.WorkplaceId == workplaceId && (x.StartDate < startDate) && (startDate < x.EndDate))
                    || (x.WorkplaceId == workplaceId && (x.StartDate < endDate) && (endDate < x.EndDate)))).Select(x => x.StartDate).ToList();

                    foreach (var item in thisWorkplaceInRecurringBooking)
                    {
                        var date = item;
                        while (date < endDate)
                        {
                            if (startDate <= date && date <= endDate)
                            {
                                isBusy = true;
                                break;
                            }
                            date = date.AddDays(7);
                        }
                        if (isBusy) break;
                    }
                }
            }

            if (isBusy)
            {
                throw new ValidationException($"Workingplace " +
                    $"{_context.Workplaces.Where(x => x.Id == workplaceId).Select(x => x.WorkplaceNumber).FirstOrDefault()}" +
                    $" is already booked for this period" +
                    $"{startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}");
            }


        }

        public async Task EnsureUserHasNotBookingThisTimeAsync(int userId, DateTimeOffset startDate, DateTimeOffset endDate, bool isRecurringBooking)
        {
            var hasBooking = false;
            if (isRecurringBooking) 
            {
                var currentBookingRecurringDays = Helper.GetRecurringDays(startDate, endDate);                
                foreach (var recurrinDayDate in currentBookingRecurringDays)
                {
                    //checkin crossing with existing not recurring bookings
                    hasBooking = await _context.Bookings.AnyAsync(x => !x.IsRecurring &&
                                   x.UserId == userId && (x.StartDate <= recurrinDayDate) && (recurrinDayDate <= x.EndDate));
                    //checkin crossing with existing recurring bookings
                    var thisUserRecurringBooking = _context.Bookings.Where(x => x.IsRecurring &&
                       x.UserId == userId && (x.StartDate <= recurrinDayDate) && (recurrinDayDate <= x.EndDate))
                       .Select(x => x.StartDate).ToList();

                    foreach (var item in thisUserRecurringBooking)
                    {
                        var date = item;
                        while (date <= recurrinDayDate)
                        {
                            if (date == recurrinDayDate)
                            {
                                hasBooking = true;
                                break;
                            }
                            date = date.AddDays(7);
                        }
                        if (hasBooking) break;
                    }
                    if (hasBooking) break;
                }
            }
            else
            {
                //checkin crossing with existing not recurring bookings
                hasBooking = await _context.Bookings.AnyAsync(x => !x.IsRecurring &&
                               ((x.UserId == userId && (x.StartDate < startDate) && (startDate < x.EndDate))
                            || (x.UserId == userId && (x.StartDate < endDate) && (endDate < x.EndDate))));
                if (!hasBooking)
                {
                    //checkin crossing with existing recurring bookings
                    var thisUserRecurringBooking = _context.Bookings.Where(x => x.IsRecurring &&
                       ((x.UserId == userId && (x.StartDate < startDate) && (startDate < x.EndDate))
                    || (x.UserId == userId && (x.StartDate < endDate) && (endDate < x.EndDate)))).Select(x => x.StartDate).ToList();

                    foreach (var item in thisUserRecurringBooking)
                    {
                        var date = item;
                        while (date < endDate)
                        {
                            if (startDate <= date && date <= endDate)
                            {
                                hasBooking = true;
                                break;
                            }
                            date = date.AddDays(7);
                        }
                        if (hasBooking) break;
                    }
                }
            }

            if (hasBooking)
            {
                throw new ValidationException($"User has another booking for this period" +
                    $"{startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}");
            }
        }

        public async Task EnsureUserHasNotVacationForThisPeriodAsync(int userId, DateTimeOffset startDate, DateTimeOffset endDate)
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
