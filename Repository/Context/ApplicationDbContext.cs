using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Country> Countries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
