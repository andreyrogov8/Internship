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
        public static List<Vacation> GetVacations(List<User> users)
        {
            return new List<Vacation>
            {
                new Vacation
                {
                    UserId = users.Where(user => user.Email == "manager@gmail.com").First().Id,
                    VacationStart = DateTime.Now,
                    VacationEnd = DateTime.Now.AddDays(15),
                    IsDeleted = false
                },

                new Vacation
                {
                    UserId = users.Where(user => user.Email == "mapeditor@gmail.com").First().Id,
                    VacationStart = DateTime.Now,
                    VacationEnd = DateTime.Now.AddDays(15),
                    IsDeleted = false
                },

                new Vacation
                {
                    UserId = users.Where(user => user.Email == "admin@gmail.com").First().Id,
                    VacationStart = DateTime.Now,
                    VacationEnd = DateTime.Now.AddDays(15),
                    IsDeleted = false
                },
            };
        }
    }
}
