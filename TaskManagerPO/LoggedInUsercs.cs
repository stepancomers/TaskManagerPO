using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TaskManagerPO
{
    public static class LoggedInUsers
    {
        public static int UserID { get; private set; }
        public static string UserRole { get; private set; }
        public static  string FullName { get; private set; }
        public static string LessonOrGroup { get; private set; }

        public static void LoggedInUsersС(int userID, string userRole)
        {
            UserID = userID;
            UserRole = userRole;
            FullDataInDbLoggedUsers();
        }

        public static void FullDataInDbLoggedUsers()
        {
            using (var dbContext = new MyDBContext())
            {
                if (UserRole == "student")
                {
                    var students = dbContext.Students;
                    foreach (var student in students)
                    {
                        if (student.StidentId == UserID)
                        {
                            FullName = student.StudentFullName;
                            LessonOrGroup = student.StudentGroup;
                        }
                    }
                }

                else if (UserRole == "teacher")
                {
                    var teachers = dbContext.Teachers;
                    foreach (var teacher in teachers)
                    {
                        if (teacher.TeacherId == UserID)
                        {
                            FullName = teacher.TeacherFullName;
                            LessonOrGroup = teacher.LessonName;
                        }
                    }
                }
                
            }
        }

    }
}
