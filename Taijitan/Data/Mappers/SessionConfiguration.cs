using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Session");
            builder.HasKey(s => s.SessionId);
            builder.HasMany(s => s.Members)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.MembersPresent)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Formulas)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
