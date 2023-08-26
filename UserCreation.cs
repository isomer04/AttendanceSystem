using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using AttendanceSystem.Data;
using AttendanceSystem.Entities;

namespace AttendanceSystem
{
    public static class UserCreation
    {
        public static void CreateUser(AttendanceDbContext context)
        {
            //Console.Write("Enter Name: ");
            //string name = Console.ReadLine();

            //Console.Write("Enter Username: ");
            //string username = Console.ReadLine();

            //Console.Write("Enter Password: ");
            //string password = Console.ReadLine();

            //Console.WriteLine("Select User Type:");
            //Console.WriteLine("1. Admin");
            //Console.WriteLine("2. Teacher");
            //Console.WriteLine("3. Student");
            //int userTypeChoice = int.Parse(Console.ReadLine());

            //UserType userType = (UserType)(userTypeChoice - 1);

            //User newUser = new User
            //{
            //    Name = name,
            //    Username = username,
            //    Password = password,
            //    UserType = userType
            //};

            //context.Users.Add(newUser);
            //context.SaveChanges();

            //Console.WriteLine("User created successfully!");



          //  ============================================================

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Select User Type:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Teacher");
            Console.WriteLine("3. Student");
            int userTypeChoice = int.Parse(Console.ReadLine());

            User newUser;

            switch ((UserType)(userTypeChoice - 1))
            {
                case UserType.Admin:
                    newUser = new Admin
                    {
                        Name = name,
                        Username = username,
                        Password = password
                    };
                    break;
                case UserType.Teacher:
                    newUser = new Teacher
                    {
                        Name = name,
                        Username = username,
                        Password = password
                    };
                    break;
                case UserType.Student:
                    newUser = new Student
                    {
                        Name = name,
                        Username = username,
                        Password = password
                    };
                    break;
                default:
                    Console.WriteLine("Invalid user type.");
                    return;
            }

            context.Users.Add(newUser);
            context.SaveChanges();

            Console.WriteLine("User created successfully!");
        }
    }
}
