using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Repository;
using System.Linq;

namespace Application.IntegrationTests;

public class TestWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    //protected override void ConfigureWebHost(IWebHostBuilder builder)
    //{
    //    builder.ConfigureServices(async services =>
    //    {
    //        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
    //        if (descriptor != null)
    //        {
    //            services.Remove(descriptor);
    //        }

    //        services.AddDbContext<ApplicationDbContext>(options =>
    //        {
    //            options.UseInMemoryDatabase("InMemoryDbForTesting");
    //        });

    //        var sp = services.BuildServiceProvider();

    //        using (var scope = sp.CreateScope())
    //        {
    //            var scopedServices = scope.ServiceProvider;
    //            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

    //            db.Database.Migrate();

    //            var userManager = scopedServices.GetRequiredService<UserManager<User>>();
    //            var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole<int>>>();
    //            await ApplicationDbContextSeed.SeedDataAsync(db, userManager, roleManager);
    //        }
    //    });
    //}
}