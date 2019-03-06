using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class FormulaTrainingDayConfiguration : IEntityTypeConfiguration<FormulaTrainingDay>
    {
        public void Configure(EntityTypeBuilder<FormulaTrainingDay> builder)
        {
            builder.ToTable("FormulaTrainingDay");
            builder.HasKey(ftd => new { ftd.FormulaId, ftd.TrainingsDayId });

            builder
                .HasOne<Formula>(ftd => ftd.Formula)
                .WithMany(f => f.FormulaTrainingDays)
                .HasForeignKey(ftd => ftd.FormulaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<TrainingDay>(ftd => ftd.TrainingDay)
                .WithMany(f => f.FormulaTrainingDays)
                .HasForeignKey(ftd => ftd.TrainingsDayId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
