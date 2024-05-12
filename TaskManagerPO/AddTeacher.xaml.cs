using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAutorization adminAutorization = new AdminAutorization();
            adminAutorization.Show();
            Close();
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {

            bool requirementsLogin = false;
            bool requirementsPassword = false;
            bool requirementsFullName = false;
            bool requirementsGroup = false;

            string errorMessage = null;

            string TeacherLogin = TeacherLoginTextBox.Text;
            if (TeacherLogin.Length >= 8)
                requirementsLogin = true;
            else
                errorMessage += "В логине меньше 8 символов" + '\n';
            string TeacherPassword = TeacherPasswordTextBox.Text;
            if (TeacherPassword.Length >= 6)
                requirementsPassword = true;
            else
                errorMessage += "В логине меньше 6 символов" + '\n';
            string fullNameTeacher = TeacherFullNameTextBox.Text;
            string[] fullNameTeacherArray = fullNameTeacher.Split(' ');
            if (fullNameTeacher.Length >= 0 && fullNameTeacherArray.Length == 2)
                requirementsFullName = true;
            else
                errorMessage += "Неверно указан формат ФИО" + '\n';
            string groupTeacher = LessonNameTextBox.Text;
            if (groupTeacher.Length <= 0)
                requirementsGroup = true;
            else
                errorMessage += "Строка названия предмета не может быть пустой";
            if (requirementsLogin && requirementsPassword && requirementsFullName && requirementsGroup)
            {
                UserAdd userAdd = new UserAdd(TeacherLogin, TeacherPassword, fullNameTeacher);
                userAdd.AddTeacherDB(groupTeacher);
                userAdd.AddUserDB();
                TeacherLoginTextBox.Text = null;
                TeacherPasswordTextBox.Text = null;
                TeacherFullNameTextBox.Text = null;
                LessonNameTextBox.Text = null;
            }

            else
            {
                MessageBox.Show(errorMessage);
            }
        }
        
    }
}
