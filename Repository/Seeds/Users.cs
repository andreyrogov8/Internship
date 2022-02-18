﻿using Domain.Enums;
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
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    TelegramId = Guid.NewGuid().ToString(),
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Jennifer",
                    LastName = "Lawrence",
                    EmploymentStart = DateTimeOffset.UtcNow,
                    EmploymentEnd = DateTimeOffset.UtcNow.AddDays(50),
                },
            
                new User
                {
                    TelegramId = Guid.NewGuid().ToString(),
                    UserName = "mapeditor@gmail.com",
                    Email = "mapeditor@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Jessica",
                    LastName = "Chastain",
                    EmploymentStart = DateTime.UtcNow,
                    EmploymentEnd = DateTime.UtcNow.AddDays(50),
                },
            
                new User
                {
                    TelegramId = Guid.NewGuid().ToString(),
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Navruz",
                    LastName = "Rakhimov",
                    EmploymentStart = DateTime.UtcNow,
                    EmploymentEnd = DateTime.UtcNow.AddDays(50),
                },
            
                new User
                {
                    TelegramId = Guid.NewGuid().ToString(),
                    UserName = "manager@gmail.com",
                    Email = "manager@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Michael",
                    LastName = "Caine",
                    EmploymentStart = DateTime.UtcNow,
                    EmploymentEnd = DateTime.UtcNow.AddDays(50),
                },
            };
        }
    }
}
