﻿using System.Windows;


namespace TaskManagerPO
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = UserLoginTextBox.Text;
            string inputPassword = UserPasswordTextBox.Text;
            if (inputUsername.Length >=0 && inputPassword.Length >= 0)
            {
                Authorization authorization = new Authorization(inputUsername, inputPassword);
                authorization.LoginUser();
                if (authorization.Succes)
                {
                    Close();
                }
                else
                {
                    UserLoginTextBox.Text = null;
                    UserPasswordTextBox.Text = null;
                }
            }
            else
                MessageBox.Show("Заполните поля правильно");
            
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminAutorization secondWindow = new AdminAutorization();
            secondWindow.Show();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TaskManagerWindow taskManagerWindow = new TaskManagerWindow();
            taskManagerWindow.Show();
            Close();
        }
    }
    
}
