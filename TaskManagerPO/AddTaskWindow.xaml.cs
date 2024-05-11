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
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        string _login;
        string _password;
        public AddTaskWindow(string login, string password)
        {
            InitializeComponent();
            _login = login;
            _password = password;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string nameTask = NameTaskTextBox.Text;
            string lessonName = LessonNameTextBox.Text;
            string groupNumber = GroupNumberTextBox.Text;
            string DescriptionTask = DescriptionTaskTextBox.Text;
            if(nameTask != null && lessonName != null && groupNumber != null && DescriptionTask != null)
            {
                AddTask addTask = new AddTask(nameTask, lessonName, groupNumber, DescriptionTask);
                addTask.AddTaskInDB();
                addTask.AddDescriprionTaskInDB();
            }
            else
            {
                MessageBox.Show("Не все данные заполнены");
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TaskManagerWindow taskManagerWindow = new TaskManagerWindow();
            taskManagerWindow.User(_login, _password);
            taskManagerWindow.Show();
            Close();
        }
    }
}
