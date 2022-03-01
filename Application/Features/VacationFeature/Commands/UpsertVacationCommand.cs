using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VacationFeature.Commands
{
    public abstract class UpsertVacationCommand
    {
        private readonly IApplicationDbContext _context;

        public UpsertVacationCommand(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task EnsureTheUserHasNotVacationInThisTimeAsync(int userId, DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken)
        {

            var userHasVacationInThisTime = true;
            userHasVacationInThisTime = await _context.Vacations.AnyAsync(v =>
           (v.UserId == userId && (v.VacationStart >= startDate) && (v.VacationStart <= endDate) && (v.VacationEnd >= endDate)) ||
           (v.UserId == userId && (v.VacationStart <= startDate) && (v.VacationEnd >= startDate) && (v.VacationEnd <= endDate)) ||
           (v.UserId == userId && (v.VacationStart >= startDate) && (v.VacationStart <= endDate) && (v.VacationEnd >= endDate)) ||
           (v.UserId == userId && (v.VacationStart >= startDate) && (v.VacationEnd <= endDate)) ||
           (v.UserId == userId && (v.VacationStart <= startDate) && (v.VacationEnd >= endDate)),
            cancellationToken);

            if (userHasVacationInThisTime)
            {
                throw new ValidationException($"You have already had vacation in this period {startDate.ToString()} {endDate.ToString()}");
            }
        }
        public async Task EnsureUserCanUpdateThisVacationAsync(int vacationId, int userId, DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken)
        {
            // exclude vacation which id = vacationId
            var userhasAnotherVacationInThisTime = await _context.Vacations.AnyAsync(v => 
           (v.UserId == userId && v.Id != vacationId && (v.VacationStart >= startDate) && (v.VacationStart <= endDate) && (v.VacationEnd >= endDate)) ||
           (v.UserId == userId && v.Id != vacationId && (v.VacationStart <= startDate) && (v.VacationEnd >= startDate) && (v.VacationEnd <= endDate)) ||
           (v.UserId == userId && v.Id != vacationId && (v.VacationStart >= startDate) && (v.VacationStart <= endDate) && (v.VacationEnd >= endDate)) ||
           (v.UserId == userId && v.Id != vacationId && (v.VacationStart >= startDate) && (v.VacationEnd <= endDate)) ||
           (v.UserId == userId && v.Id != vacationId && (v.VacationStart <= startDate) && (v.VacationEnd >= endDate)),
           cancellationToken
            );
            if (userhasAnotherVacationInThisTime)
            {
                throw new ValidationException($"Your vacations are crossed. Please, update it correctly!");

            }

        }
    } 
}
        

