using AttendanceSystem.Data;
using AttendanceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    internal class AuthenticationService
    {
       public static User AuthenticateUser()
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
    }
}
