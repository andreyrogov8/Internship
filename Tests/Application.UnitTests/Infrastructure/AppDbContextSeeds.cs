using Application.UnitTests.Seeds;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Infrastructure
{
    public class AppDbContextSeeds
    {
        public static void SeedDataAsync(ApplicationDbContext context)
        {
            TestData.AddOffices(context);
        }
    }
}
