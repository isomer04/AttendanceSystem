using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystem.Entities;
using Microsoft.Data.SqlClient;
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

            // Server=DESKTOP-47QN2Q4\SQLEXPRESS;Database=myDataBase;User Id=isomer2;Password=isomermE@.12345678;TrustServerCertificate=True

            string connectionString = "Server=DESKTOP-47QN2Q4\\SQLEXPRESS;Database=attendenceSystem;User Id=isomer2;Password=isomermE@.12345678;TrustServerCertificate=True";

            // optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: new[] { 50000, 12345 }
                );
            });



            // SqlConnection sqlConnection = new  SqlConnection(connectionString);


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and constraints here
        }
    }
}