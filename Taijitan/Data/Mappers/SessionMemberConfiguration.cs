using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class SessionMemberConfiguration : IEntityTypeConfiguration<SessionMember>
    {
        public void Configure(EntityTypeBuilder<SessionMember> builder)
        {
            builder.ToTable("SessionMember");
            builder.HasKey(sm => new {sm.SessionId, sm.MemberId });

            builder
                .HasOne(sm => sm.Member)
                .WithMany(m => m.SessionMembers)
                .HasForeignKey(sm => sm.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sm => sm.Session)
                .WithMany(m => m.SessionMembers)
                .HasForeignKey(sm => sm.SessionId)
                .OnDelete(DeleteBehavior.Restrict);
            
   
        }
    }
}
