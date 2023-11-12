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
    internal class StudentService
    {

       public static void ShowStudentMenu(User loggedInUser)
        {
            Student student = UserRepository.GetStudentByUsername(loggedInUser.Username);

            if (student != null)
            {
                Console.WriteLine("\nStudent Menu");
                Console.WriteLine("1. Give Attendance");
                Console.WriteLine("2. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        GiveAttendance(student);
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
                Console.WriteLine("User is not a student.");
            }
        }

        static void GiveAttendance(Student student)
        {
            using (var context = new AttendanceDbContext())
            {
                Console.WriteLine("Your Enrolled Courses:");
                var enrolledCourses = context.Enrollments
                    .Where(e => e.StudentId == student.Id)
                    .Select(e => e.Course)
                    .ToList();

                foreach (var course in enrolledCourses)
                {
                    Console.WriteLine($"Course ID: {course.Id}, Course Name: {course.CourseName}");
                }

                Console.Write("Enter course ID to give attendance: ");
                int courseId = int.Parse(Console.ReadLine());

                var selectedCourse = enrolledCourses.FirstOrDefault(c => c.Id == courseId);

                if (selectedCourse != null)
                {
                    var today = DateTime.Today;
                    var classSchedules = context.ClassSchedules
                        .Where(cs => cs.CourseId == selectedCourse.Id && cs.StartTime <= today.TimeOfDay && today.TimeOfDay <= cs.EndTime)
                        .ToList();

                    if (classSchedules.Any())
                    {
                        foreach (var classSchedule in classSchedules)
                        {
                            var attendance = new Attendance
                            {
                                EnrollmentId = student.Enrollments.First(e => e.CourseId == selectedCourse.Id).Id,
                                Date = today,
                                ClassNumber = classSchedule.ClassNumber,
                            };

                            context.Attendances.Add(attendance);
                        }

                        context.SaveChanges();
                        Console.WriteLine("Attendance given successfully.");
                    }
                    else
                    {

                        var nextClassSchedule = context.ClassSchedules
                        .Where(cs => cs.CourseId == selectedCourse.Id && cs.StartTime > today.TimeOfDay)
                        .OrderBy(cs => cs.StartTime)
                        .FirstOrDefault();

                        if (nextClassSchedule != null)
                        {
                            var nextClassDateTime = today.Date.Add(nextClassSchedule.StartTime);
                            string dayName = nextClassDateTime.DayOfWeek.ToString();
                            Console.WriteLine($"No classes are currently scheduled. Next class is scheduled on {dayName}, {nextClassDateTime:MM/dd/yyyy} at {nextClassSchedule.StartTime}.");
                            Console.WriteLine($"Current date and time is: {DateTime.Now:MM/dd/yyyy hh:mm tt}");

                        }
                        else
                        {
                            Console.WriteLine("There are no classes scheduled for this course.");
                        }
                        //Console.WriteLine("It's not the class time or there are no classes scheduled for today.");
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
