namespace AttendanceSystem.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } //Password can't be stored as a plain text

        //  public string HashedPassword { get; internal set; }


      //  public UserType UserType { get; set; }

        
    }


    public enum UserType
    {
        Admin,
        Teacher,
        Student
    }

}

// Teacher.cs
