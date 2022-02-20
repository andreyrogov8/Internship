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
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            await TestData.AddRoles(roleManager);
            await TestData.AddUsers(userManager, roleManager);
            await TestData.AddOffices(context);
            await TestData.AddMaps(context);
            await TestData.AddWorkplaces(context);
            await TestData.AddBookings(context);
            await TestData.AddVacations(context);
        }
    }
}
