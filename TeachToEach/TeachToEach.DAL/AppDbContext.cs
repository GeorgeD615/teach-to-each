using Microsoft.EntityFrameworkCore;
using TeachToEach.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Configurations;

namespace TeachToEach.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<TeacherStudent> TeacherStudentRelation { get; set; }

        public DbSet<StatusOfRelation> StatusOfRelations { get; set; }

        public DbSet<Homework> Homeworks { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherStudentConfiguration());
            modelBuilder.ApplyConfiguration(new StatusOfRelationsConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
        }





    }
}
