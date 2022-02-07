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

        public DbSet<Booking> Bookingings { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<SeatEquipments> SeatEquipments { get; set;}
    }
}
