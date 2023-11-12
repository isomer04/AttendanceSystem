using AttendanceSystem.Data;
using AttendanceSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    internal class AdminService
    {

        public static void ShowAdminMenu()
        {
            Console.WriteLine("\nAdmin Menu");
            Console.WriteLine("1. Create Teacher");
            Console.WriteLine("2. Create Student");
            Console.WriteLine("3. Create Course");
            Console.WriteLine("4. Assign Teacher to Course");
            Console.WriteLine("5. Assign Student to Course");
            Console.WriteLine("6. Set Class Schedule");
            Console.WriteLine("7. List of courses");
            Console.WriteLine("8. List of teachers");
            Console.WriteLine("9. List of students");
            Console.WriteLine("10. Show class Schedule");
            Console.WriteLine("11. Update Teacher");
            Console.WriteLine("12. Update Student");
            Console.WriteLine("13. Update Course");
            Console.WriteLine("14. Delete Teacher");
            Console.WriteLine("15. Delete Student");
            Console.WriteLine("16. Delete Course");
            Console.WriteLine("99. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateTeacher();
                    break;
                case 2:
                    CreateStudent();
                    break;
                case 3:
                    CreateCourse();
                    break;
                case 4:
                    AssignTeacherToCourse();
                    break;
                case 5:
                    AssignStudentToCourse();
                    break;
                case 6:
                    SetClassSchedule();
                    break;
                case 7:
                    ListCourses();
                    break;
                case 8:
                    ListTeachers();
                    break;
                case 9:
                    ListStudents();
                    break;
                case 10:
                    ShowClassSchedules();
                    break;
                case 11:
                    UpdateTeacher();
                    break;
                case 12:
                    UpdateStudent();
                    break;
                case 13:
                    UpdateCourse();
                    break;
                case 14:
                    DeleteTeacher();
                    break;
                case 15:
                    DeleteStudent();
                    break;
                case 16:
                    DeleteCourse();
                    break;
                case 99:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }


        static void CreateTeacher()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter teacher's name: ");
                string name = Console.ReadLine();

                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                var teacher = new Teacher
                {
                    Name = name,
                    Username = username,
                    Password = password
                };

                context.Teachers.Add(teacher);
                context.SaveChanges();

                Console.WriteLine("Teacher created successfully.  \n");
            }
            ShowAdminMenu();

        }

        static void CreateStudent()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter student's name: ");
                string name = Console.ReadLine();

                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                var student = new Student
                {
                    Name = name,
                    Username = username,
                    Password = password
                };

                context.Students.Add(student);
                context.SaveChanges();

                Console.WriteLine("Student created successfully.");
            }
            ShowAdminMenu();

        }

        static void CreateCourse()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter course name: ");
                string courseName = Console.ReadLine();

                Console.Write("Enter course fees: ");
                decimal fees = decimal.Parse(Console.ReadLine());

                var course = new Course
                {
                    CourseName = courseName,
                    Fees = fees
                };

                context.Courses.Add(course);
                context.SaveChanges();

                Console.WriteLine("Course created successfully.");
            }
            ShowAdminMenu();

        }

        static void AssignTeacherToCourse()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter teacher's username: ");
                string username = Console.ReadLine();

                var teacher = context.Teachers.FirstOrDefault(t => t.Username == username);

                if (teacher != null)
                {
                    Console.WriteLine("Available Courses:");
                    ListCourses();

                    Console.Write("Enter course ID to assign teacher: ");
                    int courseId = int.Parse(Console.ReadLine());

                    var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                    if (course != null)
                    {
                        course.TeacherId = teacher.Id;
                        context.SaveChanges();

                        Console.WriteLine("Teacher assigned to course successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid course ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Teacher not found.");
                }
            }
            ShowAdminMenu();

        }

        static void AssignStudentToCourse()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter student's username: ");
                string username = Console.ReadLine();

                var student = context.Students.FirstOrDefault(s => s.Username == username);

                if (student != null)
                {
                    Console.WriteLine("Available Courses:");
                    ListCourses();

                    Console.Write("Enter course ID to assign student: ");
                    int courseId = int.Parse(Console.ReadLine());

                    var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                    if (course != null)
                    {
                        var enrollment = new Enrollment
                        {
                            StudentId = student.Id,
                            CourseId = course.Id,
                            EnrollmentDate = DateTime.Now
                        };

                        context.Enrollments.Add(enrollment);
                        context.SaveChanges();

                        Console.WriteLine("Student assigned to course successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid course ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            ShowAdminMenu();

        }

        static void SetClassSchedule()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.WriteLine("Available Courses:");
                ListCourses();

                Console.Write("Enter course ID to set class schedule: ");
                int courseId = int.Parse(Console.ReadLine());

                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (course != null)
                {
                    Console.Write("Enter number of class schedules: ");
                    int numSchedules = int.Parse(Console.ReadLine());

                    for (int i = 1; i <= numSchedules; i++)
                    {
                        Console.Write($"Enter day for schedule {i} (e.g., Monday, Tuesday, ...): ");
                        string day = Console.ReadLine();

                        //Console.Write($"Enter start time for schedule {i} (HH:mm): ");
                        //string startTimeStr = Console.ReadLine();
                        //Console.WriteLine($"Debug: startTimeStr = {startTimeStr}");
                        //TimeSpan startTime = TimeSpan.ParseExact(startTimeStr, "HH:mm", CultureInfo.InvariantCulture);

                        bool startTimeParsedSuccessfully;
                        TimeSpan startTime;

                        Console.Write($"Enter start time for schedule {i} (hh:mm:ss): ");
                        string startTimeStr = Console.ReadLine();

                        startTimeParsedSuccessfully = TimeSpan.TryParseExact(startTimeStr, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out startTime);

                        if (!startTimeParsedSuccessfully)
                        {
                            Console.WriteLine("The start time was not in the correct format.");
                        }

                        Console.Write($"Enter end time for schedule {i} (hh:mm:ss): ");
                        string endTimeStr = Console.ReadLine();
                        Console.WriteLine($"Debug: endTimeStr = {endTimeStr}");
                        TimeSpan endTime = TimeSpan.ParseExact(endTimeStr, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);



                        Console.Write($"Enter total classes for schedule {i}: ");
                        int totalClasses = int.Parse(Console.ReadLine());

                        var classSchedule = new ClassSchedule
                        {
                            Day = day,
                            StartTime = startTime,
                            EndTime = endTime,
                            TotalClasses = totalClasses,
                            CourseId = course.Id
                        };

                        context.ClassSchedules.Add(classSchedule);
                    }

                    context.SaveChanges();

                    Console.WriteLine("Class schedule set successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid course ID.");
                }
            }
            ShowAdminMenu();
        }


        static void ShowClassSchedules()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.WriteLine("Available Courses:");
                ListCourses();

                Console.Write("Enter course ID to view class schedules: ");
                int courseId = int.Parse(Console.ReadLine());

                var course = context.Courses.Include(c => c.ClassSchedules).FirstOrDefault(c => c.Id == courseId);

                if (course != null)
                {
                    Console.WriteLine($"Class Schedules for Course: {course.CourseName}");

                    foreach (var schedule in course.ClassSchedules)
                    {
                        Console.WriteLine($"Day: {schedule.Day}, Start Time: {schedule.StartTime}, End Time: {schedule.EndTime}, Total Classes: {schedule.TotalClasses}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid course ID.");
                }
            }

            ShowAdminMenu();
        }


        static void ListCourses()
        {
            using (var context = new AttendanceDbContext())
            {
                var courses = context.Courses.ToList();
                foreach (var course in courses)
                {
                    Console.WriteLine($"Course ID: {course.Id}, Course Name: {course.CourseName}");
                }
            }

        }

        static void ListTeachers()
        {
            using (var context = new AttendanceDbContext())
            {
                var teachers = context.Teachers.ToList();
                Console.WriteLine("List of Teachers:");
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($"Teacher ID: {teacher.Id}, Name: {teacher.Name}");
                }
            }
            ShowAdminMenu();

        }

        static void ListStudents()
        {
            using (var context = new AttendanceDbContext())
            {
                var students = context.Students.ToList();
                Console.WriteLine("List of Students:");
                foreach (var student in students)
                {
                    Console.WriteLine($"Student ID: {student.Id}, Name: {student.Name}");
                }
            }
            ShowAdminMenu();

        }



        static void UpdateTeacher()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter teacher's username to update: ");
                string username = Console.ReadLine();

                var teacher = context.Teachers.Include(t => t.Courses).FirstOrDefault(t => t.Username == username);

                if (teacher != null)
                {
                    Console.Write("Enter new name: ");
                    teacher.Name = Console.ReadLine();

                    // Update or remove assigned courses
                    Console.WriteLine("Assigned Courses:");
                    foreach (var course in teacher.Courses)
                    {
                        Console.WriteLine($"Course ID: {course.Id}, Course Name: {course.CourseName}");
                    }

                    Console.Write("Enter course ID to update or remove (0 to skip): ");
                    int courseId = int.Parse(Console.ReadLine());

                    if (courseId != 0)
                    {
                        var course = teacher.Courses.FirstOrDefault(c => c.Id == courseId);

                        if (course != null)
                        {
                            Console.WriteLine("Do you want to (U)pdate or (R)emove the course?");
                            string choice = Console.ReadLine().ToLower();

                            if (choice == "u")
                            {
                                Console.Write("Enter new course name: ");
                                course.CourseName = Console.ReadLine();

                                Console.Write("Enter new course fees: ");
                                course.Fees = decimal.Parse(Console.ReadLine());
                            }
                            else if (choice == "r")
                            {
                                teacher.Courses.Remove(course);
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Course not found.");
                        }
                    }

                    context.SaveChanges();
                    Console.WriteLine("Teacher updated successfully.");
                }
                else
                {
                    Console.WriteLine("Teacher not found.");
                }
            }

            ShowAdminMenu();
        }

        static void UpdateStudent()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter student's username to update: ");
                string username = Console.ReadLine();

                var student = context.Students.Include(s => s.Enrollments).FirstOrDefault(s => s.Username == username);

                if (student != null)
                {
                    Console.Write("Enter new name: ");
                    student.Name = Console.ReadLine();

                    // Enroll in or remove from courses
                    Console.WriteLine("Enrolled Courses:");
                    foreach (var enrollment in student.Enrollments)
                    {
                        if (enrollment.Course != null)
                        {
                            Console.WriteLine($"Course ID: {enrollment.Course.Id}, Course Name: {enrollment.Course.CourseName}");
                        }
                        else
                        {
                            Console.WriteLine($"Enrollment with null course found.");
                        }
                    }


                    Console.Write("Enter course ID to enroll or remove (0 to skip): ");
                    int courseId = int.Parse(Console.ReadLine());

                    if (courseId != 0)
                    {
                        var enrollment = student.Enrollments.FirstOrDefault(e => e.Course != null && e.Course.Id == courseId);

                        if (enrollment != null)
                        {
                            Console.WriteLine("Do you want to (E)nroll or (R)emove from the course?");
                            string choice = Console.ReadLine().ToLower();

                            if (choice == "e")
                            {
                                // Check if student is already enrolled
                                if (student.Enrollments.Any(e => e.Course.Id == courseId))
                                {
                                    Console.WriteLine("Student is already enrolled in this course.");
                                }
                                else
                                {
                                    var course = context.Courses.FirstOrDefault(c => c.Id == courseId);
                                    if (course != null)
                                    {
                                        var newEnrollment = new Enrollment
                                        {
                                            StudentId = student.Id,
                                            CourseId = course.Id,
                                            EnrollmentDate = DateTime.Now
                                        };
                                        context.Enrollments.Add(newEnrollment);
                                        Console.WriteLine("Student enrolled in the course.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Course not found.");
                                    }
                                }
                            }
                            else if (choice == "r")
                            {
                                student.Enrollments.Remove(enrollment);
                                Console.WriteLine("Student removed from the course.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Course enrollment not found.");
                        }
                    }

                    context.SaveChanges();
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }

            ShowAdminMenu();
        }




        static void UpdateCourse()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter course ID to update: ");
                int courseId = int.Parse(Console.ReadLine());

                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (course != null)
                {
                    Console.Write("Enter new course name: ");
                    course.CourseName = Console.ReadLine();

                    Console.Write("Enter new course fees (only number like 300): ");
                    course.Fees = decimal.Parse(Console.ReadLine());

                    context.SaveChanges();
                    Console.WriteLine("Course updated successfully.");
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            }

            ShowAdminMenu();
        }


        static void DeleteTeacher()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter the ID of the teacher to remove: ");
                int teacherId = int.Parse(Console.ReadLine());

                var teacher = context.Teachers.FirstOrDefault(t => t.Id == teacherId);
                if (teacher != null)
                {
                    context.Teachers.Remove(teacher);
                    context.SaveChanges();
                    Console.WriteLine("Teacher removed successfully.");
                }
                else
                {
                    Console.WriteLine("Teacher not found.");
                }
            }
        }

        static void DeleteStudent()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter the ID of the student to remove: ");
                int studentId = int.Parse(Console.ReadLine());

                var student = context.Students.FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                    Console.WriteLine("Student removed successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
        }




        static void DeleteCourse()
        {
            using (var context = new AttendanceDbContext())
            {
                Console.Write("Enter course ID to delete: ");
                int courseId = int.Parse(Console.ReadLine());

                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (course != null)
                {
                    context.Courses.Remove(course);
                    context.SaveChanges();
                    Console.WriteLine("Course deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            }
            ShowAdminMenu();
        }
    }
}
