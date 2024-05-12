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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new MyDBContext())
            {
                string errorString = null;
                bool success = false;
                string adminLogin = AdminLoginTextBox.Text;
                bool adminLoginRequirements = false;
                string adminPassword = AdminPasswordTextBox.Text;
                bool adminPasswordRequirements = false;

                bool adminPiCodeRequirements = false;
                if (adminLogin.Length >= 5)
                    adminLoginRequirements = true;
                else
                    errorString += "Логин содержит менее 5 символов" + '\n';

                if (adminPassword.Length >= 5)
                    adminPasswordRequirements = true;
                else
                    errorString += "Пароль короче 5 символов" + '\n';

                int adminPinCode = 0;
                if (AdminCodeTextBox.Text.Length == 4)
                {
                    adminPinCode = Convert.ToInt32(AdminCodeTextBox.Text);
                    adminPiCodeRequirements = true;
                }
                else
                {
                    errorString += "Пин код должен состоять из 4 цифр";
                }
                if (adminLoginRequirements && adminPasswordRequirements && adminPiCodeRequirements)
                {
                    var admins = dbContext.Admins;
                    foreach (var admin in admins)
                    {
                        if (admin.AdminName == adminLogin && admin.AdminPassword == adminPassword && admin.AdminPinCode == adminPinCode)
                        {
                            success = true;
                        }
                        else
                            MessageBox.Show("Не верно введены данные");
                    }
                    OpenAdminPanel(success);

                }
                else
                    MessageBox.Show(errorString);
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
                string errorString = null;
                string adminLogin = AdminLoginTextBox.Text;
                bool adminLoginRequirements = false;
                string adminPassword = AdminPasswordTextBox.Text;
                bool adminPasswordRequirements = false;

                bool adminPiCodeRequirements = false;
                if (adminLogin.Length >= 5)
                    adminLoginRequirements = true;
                else
                    errorString += "Логин содержит менее 5 символов" + '\n';

                if (adminPassword.Length >= 5)
                    adminPasswordRequirements = true;
                else
                    errorString += "Пароль короче 5 символов" + '\n';

                int adminPinCode = 0;
                if (AdminCodeTextBox.Text.Length == 4)
                {
                    adminPinCode = Convert.ToInt32(AdminCodeTextBox.Text);
                    adminPiCodeRequirements = true;
                }
                else
                    errorString += "Пин код должен состоять из 4 цифр";

                if (adminLoginRequirements && adminPasswordRequirements && adminPiCodeRequirements)
                {
                    foreach (var admin in admins)
                    {
                        if (admin.AdminName == adminLogin)
                        {
                            recurringData = true;
                        }
                    }

                    if (!recurringData)
                    {
                        var newAdmin = new Admin { AdminName = adminLogin, AdminPassword = adminPassword, AdminPinCode = adminPinCode };
                        dbContext.Admins.Add(newAdmin);
                        dbContext.SaveChanges();
                        MessageBox.Show("Регистраиця прошла успешна");
                    }
                    else
                        MessageBox.Show("Админ с таким логином уже существует");
                }
                else
                    MessageBox.Show(errorString);
            }
        }

    }
}
