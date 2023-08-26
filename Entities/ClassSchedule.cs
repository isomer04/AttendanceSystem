using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Entities
{
    public class ClassSchedule
    {
        public int Id { get; set; }
        // public DayOfWeek Day { get; set; }
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TotalClasses { get; set; }

        public int ClassNumber { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }



    }

}