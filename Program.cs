using System;
using AttendanceSystem.Data;
using AttendanceSystem.Entities;

namespace AttendanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AttendanceDbContext())
            {
                Console.WriteLine("Welcome to the Attendance System!");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create User");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Implement login logic
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        // Call a method to check login credentials
                        if (CheckLoginCredentials(context, username, password))
                        {
                            Console.WriteLine("Login successful!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password.");
                        }
                        break;

                    case 2:
                        UserCreation.CreateUser(context);
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        // Method to check login credentials
        static bool CheckLoginCredentials(AttendanceDbContext context, string username, string password)
        {
           
            return username == "admin" && password == "password";
        }

   
        // static void Main(string[] args)
        // {
        //     // Authenticate user
        //     var loggedInUser = AuthenticateUser();

        //     if (loggedInUser is Admin)
        //     {
        //         // Show admin menu
        //         ShowAdminMenu();
        //     }
        //     else if (loggedInUser is Teacher)
        //     {
        //         // Show teacher menu
        //         ShowTeacherMenu((Teacher)loggedInUser);
        //     }
        //     else if (loggedInUser is Student)
        //     {
        //         // Show student menu
        //         ShowStudentMenu((Student)loggedInUser);
        //     }
        // }

        // static User AuthenticateUser()
        // {
        //     // Implement user authentication logic here
        //     // Return the authenticated user
        // }

        // static void ShowAdminMenu()
        // {
        //     // Implement admin menu options here
        //         Console.WriteLine("Admin Menu");
        //         Console.WriteLine("1. Create Teacher");
        //         Console.WriteLine("2. Create Student");
        //         Console.WriteLine("3. Create Course");
        //         Console.WriteLine("4. Assign Teacher to Course");
        //         Console.WriteLine("5. Assign Student to Course");
        //         Console.WriteLine("6. Set Class Schedule");
        // }

        // static void ShowTeacherMenu(Teacher teacher)
        // {
        //     Console.WriteLine($"Welcome, {teacher.Name}!");

        //     using (var context = new SchoolContext())
        //     {
        //         Console.WriteLine("Your Assigned Courses:");

        //         // Get courses assigned to the teacher
        //         var assignedCourses = context.Courses
        //             .Where(c => c.TeacherId == teacher.Id)
        //             .ToList();

        //         // Display assigned courses
        //         foreach (var course in assignedCourses)
        //         {
        //             Console.WriteLine($"Course ID: {course.Id}, Course Name: {course.CourseName}");
        //         }

        //         Console.Write("Select a course ID to view attendance report: ");
        //         int selectedCourseId;
        //         if (int.TryParse(Console.ReadLine(), out selectedCourseId))
        //         {
        //             // Check if the selected course is among the assigned courses
        //             var selectedCourse = assignedCourses.FirstOrDefault(c => c.Id == selectedCourseId);
        //             if (selectedCourse != null)
        //             {
        //                 // Get attendance report for the selected course
        //                 var attendanceReport = context.Attendances
        //                     .Where(a => a.Enrollment.CourseId == selectedCourse.Id)
        //                     .ToList();

        //                 // Display attendance report
        //                 Console.WriteLine($"Attendance Report for {selectedCourse.CourseName}:");
        //                 foreach (var attendance in attendanceReport)
        //                 {
        //                     string attendanceStatus = attendance.IsPresent ? "Present" : "Absent";
        //                     Console.WriteLine($"Date: {attendance.Date}, Class: {attendance.ClassNumber}, Status: {attendanceStatus}");
        //                 }
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Invalid course selection.");
        //             }
        //         }
        //         else
        //         {
        //             Console.WriteLine("Invalid input. Please enter a valid course ID.");
        //         }
        //     }
        // }


        // static void ShowStudentMenu(Student student)
        // {
        // Console.WriteLine($"Welcome, {student.Name}!");

        // using (var context = new SchoolContext())
        // {
        //     Console.WriteLine("Your Enrolled Courses:");

        //     // Get enrolled courses for the student
        //     var enrolledCourses = context.Enrollments
        //         .Where(e => e.StudentId == student.Id)
        //         .Select(e => e.Course)
        //         .ToList();

        //     // Display enrolled courses
        //     foreach (var course in enrolledCourses)
        //     {
        //         Console.WriteLine($"Course ID: {course.Id}, Course Name: {course.CourseName}");
        //     }

        //     Console.Write("Select a course ID to give attendance: ");
        //     int selectedCourseId;
        //     if (int.TryParse(Console.ReadLine(), out selectedCourseId))
        //     {
        //         // Check if the selected course is among the enrolled courses
        //         var selectedCourse = enrolledCourses.FirstOrDefault(c => c.Id == selectedCourseId);
        //         if (selectedCourse != null)
        //         {
        //             // Implement attendance entry logic
        //             Console.WriteLine($"Taking attendance for {selectedCourse.CourseName}");
        //             Console.WriteLine("Enter 'P' for present or 'A' for absent for each class:");
        //             int classNumber = 1;
        //             while (classNumber <= selectedCourse.ClassSchedules.Sum(cs => cs.TotalClasses))
        //             {
        //                 Console.Write($"Class {classNumber}: ");
        //                 string attendanceStatus = Console.ReadLine().ToUpper();
        //                 if (attendanceStatus == "P" || attendanceStatus == "A")
        //                 {
        //                     // Save attendance to the database
        //                     var enrollment = context.Enrollments.FirstOrDefault(e => e.StudentId == student.Id && e.CourseId == selectedCourse.Id);
        //                     if (enrollment != null)
        //                     {
        //                         // Create an Attendance instance and save it to the database
        //                         var attendance = new Attendance
        //                         {
        //                             EnrollmentId = enrollment.Id,
        //                             Date = DateTime.Today,
        //                             ClassNumber = classNumber,
        //                             IsPresent = attendanceStatus == "P"
        //                         };
        //                         context.Attendances.Add(attendance);
        //                         context.SaveChanges();
        //                     }
        //                     classNumber++;
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine("Invalid input. Enter 'P' for present or 'A' for absent.");
        //                 }
        //             }
        //             Console.WriteLine("Attendance recorded successfully.");
        //         }
        //         else
        //         {
        //             Console.WriteLine("Invalid course selection.");
        //         }
        //     }
        //     else
        //     {
        //         Console.WriteLine("Invalid input. Please enter a valid course ID.");
        //     }
        // }
        // }




        // static User AuthenticateUser()
        // {
        //     Console.Write("Enter username: ");  
        //     string username = Console.ReadLine();

        //     Console.Write("Enter password: ");
        //     string password = Console.ReadLine();

        //     using (var context = new SchoolContext())
        //     {
        //         // Check if the user exists in the database
        //         var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        //         if (user != null)
        //         {
        //             if (user is Admin)
        //             {
        //                 return (Admin)user;
        //             }
        //             else if (user is Teacher)
        //             {
        //                 return (Teacher)user;
        //             }
        //             else if (user is Student)
        //             {
        //                 return (Student)user;
        //             }
        //         }

        //         Console.WriteLine("Authentication failed. Invalid username or password.");
        //         return null;
        //     }
        // }

    }

    internal class SchoolContext : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
