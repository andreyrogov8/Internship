using Domain.Models;
using Domain.Models.Seats;
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
        public DbSet<Map> Floors { get; set; }
        public DbSet<Office> Offices { get; set; }

    }
}
