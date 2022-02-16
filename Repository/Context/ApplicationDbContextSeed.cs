using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;
using Persistence.Seeds;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContextSeed
    {
        public static IEnumerable<object> DefaultUsers { get; private set; }

        public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.User.ToString()));
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.Manager.ToString()));
                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.MapEditor.ToString()));
            }

            if (!await userManager.Users.AnyAsync())
            {
                foreach (var userSet in TestData.DefaultUsers)
                {
                    foreach (var user in userSet.Value)
                    {
                        var result = await userManager.CreateAsync(user, TestData.DefaultPassword);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, userSet.Key.ToString());
                            // hardcoding for now
                            if (userSet.Key == UserRole.Administrator)
                            {
                                var role = await roleManager.FindByNameAsync(UserRole.Administrator.ToString());
                                await roleManager.AddPermissionClaim(role, "Workspace");
                            }
                        }
                    }
                }
            }
        }
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<User> userManager)
        {
            await DefaultTestData.FillDB(context, userManager);
        }
    }
}
