using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class CourseMaterialConfiguration : IEntityTypeConfiguration<CourseMaterial>
    {
        public void Configure(EntityTypeBuilder<CourseMaterial> builder)
        {
            builder.ToTable("CourseMaterial");
            builder.HasKey(c => c.MaterialId);
            builder.HasMany(c => c.Images)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
