using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Vacation> Vacations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
