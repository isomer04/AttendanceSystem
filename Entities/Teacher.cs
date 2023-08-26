using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Entities
{
    public class Teacher : User
    {
        public ICollection<Course> Courses { get; set; }

    }
}