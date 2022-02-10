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
    class VacationConfiguration : IEntityTypeConfiguration<Vacation>
    {
        public void Configure(EntityTypeBuilder<Vacation> builder)
        {
            builder
                .HasOne(v => v.User)
                .WithMany(u => u.Vacations)
                .HasForeignKey(v => v.UserId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
