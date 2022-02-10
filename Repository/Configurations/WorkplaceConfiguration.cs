using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class WorkplaceConfiguration : IEntityTypeConfiguration<Workplace>
    {
        public void Configure(EntityTypeBuilder<Workplace> builder)
        {
            builder.ToTable("Workplaces");

            builder
                .HasOne(wp => wp.Map)
                .WithMany(m => m.Workplaces)
                .HasForeignKey(wp => wp.MapId)
                .OnDelete(DeleteBehavior.Cascade); // should we apply to every entity?

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
