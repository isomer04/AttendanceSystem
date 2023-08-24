using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data
{
    public class AttendanceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        // public DbSet<Teacher> Teachers { get; set; }
        // public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }


         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connection string should be here");

            // Server=DESKTOP-47QN2Q4\SQLEXPRESS;Database=myDataBase;User Id=isomer;Password=123456;TrustServerCertificate=True

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and constraints here
        }
    }
}