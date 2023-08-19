using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public decimal Fees { get; set; }

    }
}