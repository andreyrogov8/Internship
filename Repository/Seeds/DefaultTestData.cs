using Domain.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeds
{
    public static class DefaultTestData
    {
        public static List<Booking> Bookings { get; set; }
        public static List<Map> Maps { get; set; }
        public static List<Office> Offices { get; set; }
        public static List<User> Users { get; set; }
        public static List<Vacation> Vacations { get; set; }
        public static List<Workplace> Workplaces { get; set; }

        private static bool deleted = false;

        static DefaultTestData()
        {
            Bookings = new List<Booking>();
            Maps = new List<Map>();
            Offices = new List<Office>();
            Users = new List<User>();
            Vacations = new List<Vacation>();
            Workplaces = new List<Workplace>();
        }
        public static async Task FillDB(ApplicationDbContext context, UserManager<User> userManager)
        {
            var userId = (await userManager.GetUsersInRoleAsync(nameof(UserRole.Administrator))).FirstOrDefault().Id;

            var vacation = new Vacation
            {
                
                UserId = userId,
                VacationStart = DateTime.Now,
                VacationEnd = DateTime.Now.AddDays(15),
                IsDeleted = deleted

            };
            Vacations.Add(vacation);
            if (!await context.Vacations.AnyAsync())
            {
                context.Vacations.AddRange(Vacations);
            }
            await context.SaveChangesAsync();


            var office = new Office
            {
                Name = "Tbilisi office",
                Country = "Georgia",
                City = "Tbilisi",
                Address = "24 Ilo Mosashvili St, Tbilisi 0162",
                HasFreeParking = true,
                IsDeleted = deleted

            };
            Offices.Add(office);
            if (!await context.Offices.AnyAsync())
            {
                context.Offices.AddRange(Offices);
            }
            await context.SaveChangesAsync();

            var map = new Map
            {
                FloorNumber = 10,
                HasKitchen = true,
                HasMeetingRoom = true,
                IsDeleted = deleted,
                OfficeId = office.Id
            };
            Maps.Add(map);
            if (!await context.Maps.AnyAsync())
            {
                context.Maps.AddRange(Maps);
            }
            await context.SaveChangesAsync();

            var workplace = new Workplace
            {
                WorkplaceNumber = 50,
                WorkplaceType = WorkplaceType.LongTerm,
                NextToWindow = true,
                HasPC = true,
                HasMonitor = true,
                HasKeyboard = true,
                HasMouse = true,
                HasHeadset = true,
                IsDeleted = deleted,
                MapId = map.Id,
            };
            Workplaces.Add(workplace);
            if (!await context.Workplaces.AnyAsync())
            {
                context.Workplaces.AddRange(Workplaces);
            }
            await context.SaveChangesAsync();

            var booking = new Booking
            {
                UserId  = userId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsRecurring = true,
                Frequency = 5,
                IsDeleted = deleted,
                WorkplaceId = workplace.Id,
            };
            Bookings.Add(booking);

            if (!await context.Bookings.AnyAsync())
            {
                context.Bookings.AddRange(Bookings);
            }
            await context.SaveChangesAsync();


















        }
    }
}
