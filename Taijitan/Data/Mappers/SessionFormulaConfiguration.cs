using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class SessionFormulaConfiguration : IEntityTypeConfiguration<SessionFormula>
    {
        public void Configure(EntityTypeBuilder<SessionFormula> builder)
        {
            builder.ToTable("SessionFormula");
            builder.HasKey(sm => new { sm.SessionId, sm.FormulaId });

            builder
                .HasOne(sm => sm.Formula)
                .WithMany(m => m.SessionFormulas)
                .HasForeignKey(sm => sm.FormulaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sm => sm.Session)
                .WithMany(m => m.SessionFormulas)
                .HasForeignKey(sm => sm.SessionId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
