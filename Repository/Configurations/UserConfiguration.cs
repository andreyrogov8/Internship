using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder
            //    .HasMany(u => u.Bookings)
            //    .WithOne(b => b.User);

            //builder
            //    .HasMany(u => u.Vacations)
            //    .WithOne(v => v.User);
            throw new NotImplementedException();
        }
    }
}
