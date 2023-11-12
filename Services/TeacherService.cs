using AttendanceSystem.Data;
using AttendanceSystem.Entities;
using AttendanceSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    internal class TeacherService
    {
        private readonly AttendanceDbContext _context;


       public static void ShowTeacherMenu(User loggedInUser)
        {
            Teacher teacher = UserRepository.GetTeacherByUsername(loggedInUser.Username);

            if (teacher != null)
            {
                Console.WriteLine("Teacher Menu");
                Console.WriteLine("1. Check Attendance Reports");
                Console.WriteLine("2. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CheckAttendanceReports(teacher);
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("User is not a teacher.");
            }
        }

        public static void CheckAttendanceReports(Teacher teacher)
        {
            using (var context = new AttendanceDbContext())
            {
                Console.WriteLine("Your Assigned Courses:");
                var assignedCourses = context.Courses.Where(c => c.TeacherId == teacher.Id).ToList();

                foreach (var course in assignedCourses)
                {
                    Console.WriteLine($"Course ID: {course.Id}, Course Name: {course.CourseName}");
                }

                Console.Write("Enter course ID to view attendance report: ");
                int courseId = int.Parse(Console.ReadLine());

                var selectedCourse = context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (selectedCourse != null)
                {
                    var attendanceReport = context.Attendances
                        .Where(a => a.Enrollment.CourseId == selectedCourse.Id)
                        .ToList();

                    Console.WriteLine($"Attendance Report for {selectedCourse.CourseName}:");
                    foreach (var attendance in attendanceReport)
                    {
                        string attendanceStatus = attendance.IsPresent ? "Present" : "Absent";
                        Console.WriteLine($"Date: {attendance.Date}, Class: {attendance.ClassNumber}, Status: {attendanceStatus}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid course ID.");
                }
            }

        }
    }
}
