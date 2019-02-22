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
        public DbSet<User> Users_Domain { get; set; }
        //vreemde naamgeving omdat EF ook een tabel heeft die User heet
        //deze zit ook in deze ApplicationDbContext door de overerving

        public DbSet<Member> Members { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<City> Cities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            
        }
    }
}
