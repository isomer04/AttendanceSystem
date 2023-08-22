using System;
using AttendanceSystem.Data;

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
    }
}
