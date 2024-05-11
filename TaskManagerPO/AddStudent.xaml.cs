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
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            string studentLogin = StudentLoginTextBox.Text;
            string studentPassword = StudentPasswordTextBox.Text;
            string fullNameStudent = StudentFullNameTextBox.Text;
            string groupStudent = StudentGroupTextBox.Text;

            UserAdd userAdd = new UserAdd(studentLogin, studentPassword, fullNameStudent);
            userAdd.AddStudentDB(groupStudent);
            userAdd.AddUserDB();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAutorization adminAutorization = new AdminAutorization();
            adminAutorization.Show();
            Close();
        }
    }
}
