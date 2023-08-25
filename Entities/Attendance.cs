using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassScheduleId { get; set; }
        public bool IsPresent { get; set; }
        public int EnrollmentId { get; internal set; }
        public DateTime Date { get; internal set; }
        public int ClassNumber { get; internal set; }
        public Enrollment Enrollment { get; internal set; }
    }

}