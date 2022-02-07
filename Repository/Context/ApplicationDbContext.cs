using Domain.Models;
using Domain.Models.Seats;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<SeatEquipments> SeatEquipments { get; set;}
    }
}
