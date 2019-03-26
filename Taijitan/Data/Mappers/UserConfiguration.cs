using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("User");
                builder.HasKey(m => m.UserId);
            builder.HasOne(m => m.City)
                .WithMany()
                .IsRequired();
            builder.HasMany(c => c.Comments)
                .WithOne(c => c.Member)
                .OnDelete(DeleteBehavior.Cascade);
        }
        }
    }
