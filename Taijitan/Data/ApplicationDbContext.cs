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
        public DbSet<Session> Sessions { get; set; }
        public DbSet<TrainingDay> TrainingDays { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<NonMember> NonMembers { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Score> Scores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new SessionConfiguration());
            builder.ApplyConfiguration(new TrainingDayConfiguration());
            builder.ApplyConfiguration(new FormulaConfiguration());
            builder.ApplyConfiguration(new SessionMemberConfiguration());
            builder.ApplyConfiguration(new FormulaTrainingDayConfiguration());
            builder.ApplyConfiguration(new NonMemberConfiguration());
            builder.ApplyConfiguration(new SessionFormulaConfiguration());
            builder.ApplyConfiguration(new CourseMaterialConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new ActivityConfiguration());
            builder.ApplyConfiguration(new ActivityMemberConfiguration());
            builder.ApplyConfiguration(new ScoreConfiguration());
        }
    }
}
