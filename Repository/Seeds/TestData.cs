using Application.Telegram;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class TestData
    {
        public const string DefaultPassword = "Pa$$w0rd";

        public static Dictionary<UserRole, List<User>> DefaultUsers = new Dictionary<UserRole, List<User>>()
        {
            {
                UserRole.User,
                new List<User>
                {
                    new User
                    {
                        TelegramId = Guid.NewGuid().ToString(),
                        UserName = "basicuser@gmail.com",
                        Email = "basicuser@gmail.com",
                        EmailConfirmed = true,
                        FirstName = "Jennifer",
                        LastName = "Lawrence",
                        EmploymentStart = DateTimeOffset.UtcNow,
                        EmploymentEnd = DateTimeOffset.UtcNow.AddDays(30),
                    },
                }
            },

            {
                UserRole.Administrator,
                new List<User>
                {
                    new User
                    {
                        TelegramId = Guid.NewGuid().ToString(),
                        UserName = "superadmin@gmail.com",
                        Email = "superadmin@gmail.com",
                        EmailConfirmed = true,
                        FirstName = "Navruz",
                        LastName = "Rakhimov",
                        EmploymentStart = DateTime.UtcNow,
                        EmploymentEnd = DateTime.UtcNow.AddDays(50),
                    },
                }
            },
            
        };
    }
}
