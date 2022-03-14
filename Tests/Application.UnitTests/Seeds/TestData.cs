using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Seeds
{
    public partial class TestData
    {
        public static void AddOffices(ApplicationDbContext context)
        {
            if (!context.Offices.Any())
            {
                context.Offices.AddRange(GetOffices());
                context.SaveChanges();
            }
        }
    }
}
