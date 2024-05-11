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
    /// Логика взаимодействия для ChoosRole.xaml
    /// </summary>
    public partial class ChoosRole : Window
    {
        public ChoosRole()
        {
            InitializeComponent();
        }

        private void StudentAdd_Click(object sender, RoutedEventArgs e)
        {
            AdminAddUsers adminAddUsers = new AdminAddUsers();
            adminAddUsers.Show();
            Close();
        }

        private void TeacherAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTeacher addTeacher = new AddTeacher();
            addTeacher.Show();
            Close();
        }
    }
}
