using AttendanceSystem.Data;
using AttendanceSystem.Entities;
using AttendanceSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AttendanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {

/*          AdminService adminService = new AdminService();
            StudentService studentService = new StudentService();
            TeacherService  teacherService = new TeacherService();*/

            User loggedInUser =  AuthenticationService.AuthenticateUser();

            if (loggedInUser != null)
            {
                Console.WriteLine($"Welcome, {loggedInUser.Name}!");

                if (loggedInUser is Admin)
                {

                    AdminService.ShowAdminMenu();
                }
                else if (loggedInUser is Teacher)
                {
                    TeacherService.ShowTeacherMenu((Teacher)loggedInUser);
                }
                else if (loggedInUser is Student)
                {
                    StudentService.ShowStudentMenu((Student)loggedInUser);
                }
                else
                {
                    Console.WriteLine("Unknown user type.");
                }
            }
        }

    }
}
