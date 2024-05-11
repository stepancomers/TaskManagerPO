using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManagerPO.AddUser;

namespace TaskManagerPO
{
    public class Authorization
    {
        public bool Succes;
        string _login;
        string _password;
        string _Fullname;
        string _role;
        public Authorization(string login, string password)
        {
            _login = login;
            _password = password;
            Succes = false;
        }

        public void LoginUser()
        {
            using (var dbContext = new MyDBContext())
            {
                var users = dbContext.Users;
                foreach (var user in users)
                {
                    if (user.UserName == _login && user.UserPassword == _password)
                    {
                        LoggedInUsers.LoggedInUsersС(user.UserId, user.UserRole);
                        TaskManagerWindow taskManagerWindow = new TaskManagerWindow();
                        taskManagerWindow.User(user.UserName, user.UserPassword);
                        taskManagerWindow.Show();
                        Succes = true;
                    }
                }
                if (!Succes)
                {
                    MessageBox.Show("User not found");
                }
            }
        }
    }
}
