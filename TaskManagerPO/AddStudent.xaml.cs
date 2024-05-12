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
using System.Text.RegularExpressions;

namespace TaskManagerPO
{
    /// <summary>
    /// Логика взаимодействия для AdminAddUsers.xaml
    /// </summary>
    public partial class AdminAddUsers : Window
    {
        public AdminAddUsers()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            bool requirementsLogin = false;
            bool requirementsPassword = false;
            bool requirementsFullName = false;
            bool requirementsGroup = false;

            string errorMessage = null;
            string studentLogin = StudentLoginTextBox.Text;
            if (studentLogin.Length >= 8)
                requirementsLogin = true;
            else
                errorMessage += "В логине меньше 8 символов" + '\n';
            string studentPassword = StudentPasswordTextBox.Text;
            if(studentPassword.Length >= 6)
                requirementsPassword = true; 
            else
                errorMessage += "В логине меньше 6 символов" +'\n';
            string fullNameStudent = StudentFullNameTextBox.Text;
            string[] fullNameStudentArray = fullNameStudent.Split(' ');
            if (fullNameStudent.Length >= 0 && fullNameStudentArray.Length == 2)
                requirementsFullName = true;
            else
                errorMessage += "Неверно указан формат ФИО" + '\n';
            string pattern = @"^\d[а-я]{1,2}\d$";
            string groupStudent = StudentGroupTextBox.Text;
            bool isValid = Regex.IsMatch(groupStudent, pattern);
            if (isValid)
                requirementsGroup = true;
            else
                errorMessage += "Неверно указан формат номера группы.";
            if (requirementsLogin && requirementsPassword && requirementsFullName && requirementsGroup)
            {
                UserAdd userAdd = new UserAdd(studentLogin, studentPassword, fullNameStudent);
                userAdd.AddStudentDB(groupStudent);
                userAdd.AddUserDB();
                StudentLoginTextBox.Text = null;
                StudentPasswordTextBox.Text = null;
                StudentFullNameTextBox.Text = null;
                StudentGroupTextBox.Text = null;
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAutorization adminAutorization = new AdminAutorization();
            adminAutorization.Show();
            Close();
        }
    }
}
