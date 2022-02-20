using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static partial class TestData
    {
        public static List<Booking> GetBookings(List<User> users, List<Workplace> workplaces)
        {
            return new List<Booking>
            {
                new Booking
                {
                    UserId  = users.Where(user => user.Email == "user@gmail.com").First().Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    IsRecurring = true,
                    Frequency = 5,
                    IsDeleted = false,
                    WorkplaceId = workplaces.Where(wp => wp.WorkplaceNumber == 1).First().Id,
                },

                new Booking
                {
                    UserId  = users.Where(user => user.Email == "mapeditor@gmail.com").First().Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    IsRecurring = true,
                    Frequency = 5,
                    IsDeleted = false,
                    WorkplaceId = workplaces.Where(wp => wp.WorkplaceNumber == 2).First().Id,
                },

                new Booking
                {
                    UserId  = users.Where(user => user.Email == "admin@gmail.com").First().Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    IsRecurring = true,
                    Frequency = 5,
                    IsDeleted = false,
                    WorkplaceId = workplaces.Where(wp => wp.WorkplaceNumber == 3).First().Id,
                },

                new Booking
                {
                    UserId  = users.Where(user => user.Email == "manager@gmail.com").First().Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    IsRecurring = true,
                    Frequency = 5,
                    IsDeleted = false,
                    WorkplaceId = workplaces.Where(wp => wp.WorkplaceNumber == 4).First().Id,
                },
            };
        }
    }
}
