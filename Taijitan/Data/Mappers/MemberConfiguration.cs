using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Member");
            builder.HasKey(m => m.MemberId);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Firstname)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(m => m.Email)
               .IsRequired()
               .HasMaxLength(100);
        }
    }
}
