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
using TaskManagerPO.AddUser;

namespace TaskManagerPO
{
    /// <summary>
    /// Логика взаимодействия для AdminAutorization.xaml
    /// </summary>
    public partial class AdminAutorization : Window
    {
        public AdminAutorization()
        {
            InitializeComponent();
        }

        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new MyDBContext())
            {
                bool success = false;
                string adminLogin = AdminLoginTextBox.Text;
                string adminPassword = AdminPasswordTextBox.Text;
                int adminPinCode = Convert.ToInt32(AdminCodeTextBox.Text);
                var admins = dbContext.Admins;
                foreach (var admin in admins)
                {
                    if (admin.AdminName == adminLogin && admin.AdminPassword == adminPassword && admin.AdminPinCode == adminPinCode)
                    {
                        success = true;
                    }
                }
                OpenAdminPanel(success);
            }
        }

        void OpenAdminPanel(bool success)
        {
            if(success) 
            { 
                ChoosRole choosRole = new ChoosRole();
                choosRole.Show();
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AutorizationWindow autorizationWindow = new AutorizationWindow();
            autorizationWindow.Show();
            Close();
        }

        private void AdminReg_Click(object sender, RoutedEventArgs e)
        {
            bool recurringData = false;
            using (var dbContext = new MyDBContext())
            {
                dbContext.Database.CreateIfNotExists();
                var admins = dbContext.Admins;
                string adminLogin = AdminLoginTextBox.Text;
                string adminPassword = AdminPasswordTextBox.Text;
                int adminPinCode = Convert.ToInt32(AdminCodeTextBox.Text);

                foreach (var admin in admins)
                {
                    if (admin.AdminName == adminLogin)
                    {
                        recurringData = true;
                    }
                }

                if (!recurringData)
                {
                    var newAdmin = new Admin { AdminName = adminLogin, AdminPassword = adminPassword, AdminPinCode = adminPinCode};
                    dbContext.Admins.Add(newAdmin);
                    dbContext.SaveChanges();
                    MessageBox.Show("successfully registered");
                }
                else
                    MessageBox.Show("admin with this logiin already exists");
            }
        }

    }
}
