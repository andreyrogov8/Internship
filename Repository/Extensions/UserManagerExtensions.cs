using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<bool> CheckTelegramIdAsync(this UserManager<User> userManager, User user, int telegramId)
        {
            return await userManager.Users.AnyAsync(u => u.TelegramId == telegramId && u.Email == user.Email);
        }
    }
}
