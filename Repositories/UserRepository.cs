using AttendanceSystem.Data;
using AttendanceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Repositories
{
    internal class UserRepository
    {
        public static Student GetStudentByUsername(string username)
        {
            using (var context = new AttendanceDbContext())
            {
                return context.Students.FirstOrDefault(s => s.Username == username);
            }
        }
    }
}
