using Application.Telegram;
using Domain.Enums;
using Domain.Models;

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
                        TelegramId = "644230165",
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
