using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManagerPO
{
    public class UserAdd
    {
        string _userLogin;
        string _UserPassword;
        string _userFullName;
        bool _teacher = false;
        bool _student = false;

        public UserAdd(string userLogin, string userPassword, string userFullName)
        {
            _userLogin = userLogin;
            _UserPassword = userPassword;
            _userFullName = userFullName;
        }

        public void AddStudentDB(string groupName)
        {
            _student = true;
            using (var dbContext = new MyDBContext())
            {
                dbContext.Database.CreateIfNotExists();
                var students = dbContext.Students;
                bool recuringData = false;
                foreach (var student in students)
                {
                    if (student.StudentUserName == _userLogin)
                    {
                        recuringData = true;
                    }
                }

                if (!recuringData)
                {
                    var newStudent = new Student { StudentUserName = _userLogin, StudentPassword = _UserPassword, StudentFullName = _userFullName, StudentGroup = groupName };
                    dbContext.Students.Add(newStudent);
                    dbContext.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен");
                }

                else
                    MessageBox.Show("Студент с таким логином уже существует");
            }
        }

        public void AddTeacherDB(string lessonName)
        {
            _teacher = true;
            using (var dbContext = new MyDBContext())
            {
                dbContext.Database.CreateIfNotExists();
                var teachers = dbContext.Teachers;
                bool recuringData = false;
                foreach (var teacher in teachers)
                {
                    if (teacher.TeacherUserName == _userLogin)
                    {
                        recuringData = true;
                    }
                }

                if (!recuringData)
                {
                    var newTeacher = new Teacher { TeacherUserName = _userLogin, TeacherPassword = _UserPassword, TeacherFullName = _userFullName, LessonName = lessonName };
                    dbContext.Teachers.Add(newTeacher);
                    dbContext.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен");
                }
                else
                    MessageBox.Show("Преподователь с таким логином уже существует");
            }
        }

        public void AddUserDB()
        {
            using (var dbContext = new MyDBContext())
            {
                string userRole = null;
                if (_teacher)
                    userRole = "teacher";
                else if (_student)
                    userRole = "student";
                dbContext.Database.CreateIfNotExists();
                var newUser = new User { UserName = _userLogin, UserPassword = _UserPassword, UserRole = userRole };
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
            }
        }
    }
}
