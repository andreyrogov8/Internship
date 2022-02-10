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
    class MapConfiguration : IEntityTypeConfiguration<Map>
    {
        public void Configure(EntityTypeBuilder<Map> builder)
        {
            builder.ToTable("Maps");

            builder
                .HasOne(m => m.Office)
                .WithMany(o => o.Maps)
                .HasForeignKey(m => m.OfficeId);

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}
