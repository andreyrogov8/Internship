using Application.Telegram;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public partial class TestData
    {
        public const string DefaultPassword = "Pa$$w0rd";

        public static async Task AddRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.User.ToString()));
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.Manager.ToString()));
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.MapEditor.ToString()));
            }
        }

        public static async Task AddUsers(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var roles = Enum.GetNames(typeof(UserRole));
                var usersAndRoles = GetUsers().Zip(roles, (u, r) => new { User = u, Role = r});

                foreach (var ur in usersAndRoles)
                {
                    var result = await userManager.CreateAsync(ur.User, DefaultPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(ur.User, ur.Role);
                    }
                }
            }
        }

        public static async Task AddOffices(ApplicationDbContext context)
        {
            if (!await context.Offices.AnyAsync())
            {
                context.Offices.AddRange(GetOffices());
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddMaps(ApplicationDbContext context)
        {
            if (!await context.Maps.AnyAsync())
            {
                var offices = await context.Offices.ToListAsync();
                context.Maps.AddRange(GetMaps(offices));
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddWorkplaces(ApplicationDbContext context)
        {
            if (!await context.Workplaces.AnyAsync())
            {
                var maps = await context.Maps.ToListAsync();
                context.Workplaces.AddRange(GetWorkplaces(maps));
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddBookings(ApplicationDbContext context)
        {
            if (!await context.Bookings.AnyAsync())
            {
                var users = await context.Users.ToListAsync();
                var workplaces = await context.Workplaces.ToListAsync();

                context.Bookings.AddRange(GetBookings(users, workplaces));
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddVacations(ApplicationDbContext context)
        {
            if (!await context.Vacations.AnyAsync())
            {
                var users = await context.Users.ToListAsync();
                context.Vacations.AddRange(GetVacations(users));
                await context.SaveChangesAsync();
            }
        }
    }
}
