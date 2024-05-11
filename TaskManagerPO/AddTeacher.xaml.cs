using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManagerPO.AddUser
{
    /// <summary>
    /// Логика взаимодействия для AddTeacher.xaml
    /// </summary>
    public partial class AddTeacher : Window
    {
        public AddTeacher()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAutorization adminAutorization = new AdminAutorization();
            adminAutorization.Show();
            Close();
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {

            string teacherLogin = TeacherLoginTextBox.Text;
            string teacherPassword = TeacherPasswordTextBox.Text;
            string fullNameTeacher = TeacherFullNameTextBox.Text;
            string lessonName = LessonNameTextBox.Text;

            UserAdd userAdd = new UserAdd(teacherLogin, teacherPassword, fullNameTeacher);
            userAdd.AddTeacherDB(lessonName);
            userAdd.AddUserDB();
        }
        
    }
}
