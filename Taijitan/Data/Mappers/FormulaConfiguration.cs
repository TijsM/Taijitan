using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class FormulaConfiguration : IEntityTypeConfiguration<Formula>
    {
        public void Configure(EntityTypeBuilder<Formula> builder)
        {
            builder.ToTable("Formula");
            builder.HasKey(f => f.FormulaId);
            builder.HasMany(f => f.FormulaTrainingDays)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
