using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taijitan.Data.Mappers;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<City> Cities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MemberConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            
        }
    }
}
