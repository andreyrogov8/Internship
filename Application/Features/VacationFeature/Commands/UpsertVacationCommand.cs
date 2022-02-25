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
        public void EnsureTheUserHasNotVacationInThisTime(int userId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            
            var userHasVacationInThisTime = true;
            userHasVacationInThisTime  = _context.Vacations.Where(v => 
            (v.UserId == userId && (v.VacationStart >= startDate) && (v.VacationEnd >= endDate)) ||
            (v.UserId == userId && (v.VacationStart <= startDate) && (v.VacationEnd >= startDate) && (v.VacationEnd <= endDate)) ||
            (v.UserId == userId && (v.VacationStart >= startDate) && (v.VacationStart <= endDate) && (v.VacationEnd >= endDate)) ||
            (v.UserId == userId && (v.VacationStart >= startDate) && (v.VacationEnd <= endDate))
            ).Any();
            Console.WriteLine(userHasVacationInThisTime);

            if (userHasVacationInThisTime)
            {
                throw new ValidationException($"You have already had vacation in this period {startDate.ToString()} {endDate.ToString()}");
            }
        }
    }
}
