
namespace AttendanceSystem.Menus
{
    internal class AdminMenu
    {

        static void ShowAdminMenu()
        {
            Console.WriteLine("\nAdmin Menu");
            Console.WriteLine("1. Create Teacher");
            Console.WriteLine("2. Create Student");
            Console.WriteLine("3. Create Course");
            Console.WriteLine("4. Assign Teacher to Course");
            Console.WriteLine("5. Assign Student to Course");
            Console.WriteLine("6. Set Class Schedule");
            Console.WriteLine("7. List of courses");
            Console.WriteLine("8. List of teachers");
            Console.WriteLine("9. List of students");
            Console.WriteLine("10. Show class Schedule");
            Console.WriteLine("11. Update Teacher");
            Console.WriteLine("12. Update Student");
            Console.WriteLine("13. Update Course");
            Console.WriteLine("14. Delete Teacher");
            Console.WriteLine("15. Delete Student");
            Console.WriteLine("16. Delete Course");
            Console.WriteLine("99. Exit");

            int choice = int.Parse(Console.ReadLine());
/*
            switch (choice)
            {
                case 1:
                    CreateTeacher();
                    break;
                case 2:
                    CreateStudent();
                    break;
                case 3:
                    CreateCourse();
                    break;
                case 4:
                    AssignTeacherToCourse();
                    break;
                case 5:
                    AssignStudentToCourse();
                    break;
                case 6:
                    SetClassSchedule();
                    break;
                case 7:
                    ListCourses();
                    break;
                case 8:
                    ListTeachers();
                    break;
                case 9:
                    ListStudents();
                    break;
                case 10:
                    ShowClassSchedules();
                    break;
                case 11:
                    UpdateTeacher();
                    break;
                case 12:
                    UpdateStudent();
                    break;
                case 13:
                    UpdateCourse();
                    break;
                case 14:
                    DeleteTeacher();
                    break;
                case 15:
                    DeleteStudent();
                    break;
                case 16:
                    DeleteCourse();
                    break;
                case 99:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }*/
        }
    }
}
