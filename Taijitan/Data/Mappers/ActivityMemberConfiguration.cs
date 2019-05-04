using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class ActivityMemberConfiguration : IEntityTypeConfiguration<ActivityMember>
    {
        public void Configure(EntityTypeBuilder<ActivityMember> builder)
        {
            builder.ToTable("ActivityMember");
            builder.HasKey(am => new { am.ActivityId, am.MemberId });

            builder
               .HasOne(am => am.Activity)
               .WithMany(a => a.ActivityMembers)
               .HasForeignKey(am => am.ActivityId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(am => am.Member)
                .WithMany(m => m.ActivityMembers)
                .HasForeignKey(am => am.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
