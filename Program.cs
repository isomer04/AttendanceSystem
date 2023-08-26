using System;
using AttendanceSystem.Data;
using AttendanceSystem.Entities;
using System.Linq;


namespace AttendanceSystem
{
    class Program
    {
        // static void Main(string[] args)
        // {
        //     User loggedInUser = AuthenticateUser();

        //     if (loggedInUser != null)
        //     {
        //         Console.WriteLine($"Welcome, {loggedInUser.Name}!");

        //         switch (loggedInUser.UserType)
        //         {
        //             case UserType.Admin:
        //                 // Call admin menu function
        //                 ShowAdminMenu();
        //                 break;
        //             case UserType.Teacher:
        //                 // Call teacher menu function
        //                 ShowTeacherMenu(loggedInUser);
        //                 break;
        //             case UserType.Student:
        //                 // Call student menu function
        //                 ShowStudentMenu(loggedInUser);
        //                 break;
        //             default:
        //                 Console.WriteLine("Unknown user type.");
        //                 break;
        //         }
        //     }


        // }

        static void Main(string[] args)
        {
            User loggedInUser = AuthenticateUser();

            if (loggedInUser != null)
            {
                Console.WriteLine($"Welcome, {loggedInUser.Name}!");

                if (loggedInUser is Admin)
                {
                    ShowAdminMenu();
                }
                else if (loggedInUser is Teacher)
                {
                    ShowTeacherMenu((Teacher)loggedInUser);
                }
                else if (loggedInUser is Student)
                {
                    ShowStudentMenu((Student)loggedInUser);
                }
                else
                {
                    Console.WriteLine("Unknown user type.");
                }
            }
        }


        static User AuthenticateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            using (var context = new AttendanceDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    return user;
                }

                Console.WriteLine("Authentication failed. Invalid username or password.");
                return null;
            }
        }

        static void ShowAdminMenu()
        {
            Console.WriteLine("Admin Menu");
            Console.WriteLine("1. Create Teacher");
            Console.WriteLine("2. Create Student");
            Console.WriteLine("3. Create Course");
            Console.WriteLine("4. Assign Teacher to Course");
            Console.WriteLine("5. Assign Student to Course");
            Console.WriteLine("6. Set Class Schedule");
            Console.WriteLine("7. Exit");

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
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void ShowTeacherMenu(User loggedInUser)
        {
            Teacher teacher = GetTeacherByUsername(loggedInUser.Username);

            if (teacher != null)
            {
                Console.WriteLine($"Welcome, {teacher.Name}!");

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

        static void ShowStudentMenu(User loggedInUser)
        {
            Student student = GetStudentByUsername(loggedInUser.Username);

            if (student != null)
            {
                Console.WriteLine($"Welcome, {student.Name}!");

                Console.WriteLine("Student Menu");
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

                Console.WriteLine("Teacher created successfully.");
            }
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

                        Console.Write($"Enter start time for schedule {i} (e.g., 08:00 AM): ");
                        TimeSpan startTime = TimeSpan.Parse(Console.ReadLine());

                        Console.Write($"Enter end time for schedule {i} (e.g., 10:00 AM): ");
                        TimeSpan endTime = TimeSpan.Parse(Console.ReadLine());

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
        }

        static void CheckAttendanceReports(Teacher teacher)
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
                                ClassNumber = classSchedule.ClassNumber, // Set the class number appropriately
                                IsPresent = true // You can implement attendance marking logic here
                            };

                            context.Attendances.Add(attendance);
                        }

                        context.SaveChanges();
                        Console.WriteLine("Attendance given successfully.");
                    }
                    else
                    {
                        Console.WriteLine("It's not the class time or there are no classes scheduled for today.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid course ID.");
                }
            }
        }

        static Teacher GetTeacherByUsername(string username)
        {
            using (var context = new AttendanceDbContext())
            {
                return context.Teachers.FirstOrDefault(t => t.Username == username);
            }
        }

        static Student GetStudentByUsername(string username)
        {
            using (var context = new AttendanceDbContext())
            {
                return context.Students.FirstOrDefault(s => s.Username == username);
            }
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





    }



}
