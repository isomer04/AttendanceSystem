using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
      //  public string HashedPassword { get; internal set; }
    }


    // Teacher.cs
    // public class Teacher : User { }

    // // Student.cs
    // public class Student : User { }

    // // Admin.cs
    // public class Admin : User { }

    public enum UserType
{
    Admin,
    Teacher,
    Student
}
}