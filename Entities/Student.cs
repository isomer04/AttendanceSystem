using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Entities
{
    public class Student : User
    {
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}