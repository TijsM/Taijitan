using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class NonMemberConfiguration : IEntityTypeConfiguration<NonMember>
    {
        public void Configure(EntityTypeBuilder<NonMember> builder)
        {
            builder.ToTable("NonMember");
            builder.HasKey(nm => nm.Id);
        }
    }
}
