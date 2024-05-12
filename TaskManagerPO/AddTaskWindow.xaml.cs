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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _login = login;
            _password = password;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            bool requirementsnameTask = false;
            bool requirementslessonName = false;
            bool requirementsgroupNumber = false;
            bool requirementsDescriptionTask = false;

            string errorMessage = null;
            string nameTask = NameTaskTextBox.Text;
            if (nameTask.Length >= 10)
                requirementsnameTask = true;
            else
                errorMessage += "В название меньше 10 символов" + '\n';

            string lessonName = LessonNameTextBox.Text;
            if (lessonName.Length >= 0)
                requirementslessonName = true;
            else
                errorMessage += "Строка названия предмета не должна быть пустой" + '\n';

            string DescriptionTask = DescriptionTaskTextBox.Text;
            if (DescriptionTask.Length > 20)
                requirementsDescriptionTask = true;
            else
                errorMessage += "В описании задания меньше 20 символов" + '\n';

            string pattern = @"^\d[а-я]{1,2}\d$";
            string groupNumber = GroupNumberTextBox.Text;
            bool isValid = Regex.IsMatch(groupNumber, pattern);

            if (isValid)
                requirementsgroupNumber = true;
            else
                errorMessage += "Неверно указан формат номера группы";
            if (requirementsnameTask && requirementslessonName && requirementsgroupNumber && requirementsDescriptionTask)
            {
                AddTask addTask = new AddTask(nameTask, lessonName, groupNumber, DescriptionTask);
                addTask.AddTaskInDB();
                addTask.AddDescriprionTaskInDB();
            }

            else
            {
                MessageBox.Show(errorMessage);
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
