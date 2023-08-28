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

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            string connectionString = "Server=DESKTOP-47QN2Q4\\SQLEXPRESS;Database=attendenceSystem;User Id=isomer2;Password=isomermE@.12345678;TrustServerCertificate=True"; //TODO: Needs to move this in the appsettings.json file

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: new[] { 50000, 12345 }
                    );
                });
            }



        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)


        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")  // Use a string discriminator column
                .HasValue<Admin>("Admin")
                .HasValue<Teacher>("Teacher")
                .HasValue<Student>("Student");
        }
    }
}