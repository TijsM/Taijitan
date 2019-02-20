using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Lid> Leden { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MemberConfiguration());
        }
    }
}
