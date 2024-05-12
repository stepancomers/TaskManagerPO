using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace TaskManagerPO
{
    /// <summary>
    /// Логика взаимодействия для TaskManagerWindow.xaml
    /// </summary>
    public partial class TaskManagerWindow : Window
    {
        public TaskManagerWindow()
        {
            InitializeComponent();

        }

        string _role;
        string _login;
        string _password;
        string _lessonNameOrGroup;
        string _taskDescription;
        bool _succes = false;

        void RoleInfoInWindow()
        {
            if (_role == "teacher")
            {
                RoleInfo.Text = "teacher";
            }
            else if (_role == "student")
            {
                RoleInfo.Text = "student";
            }
        }

        public void User(string login, string password)
        {
            using (var dbContext = new MyDBContext())
            {
                var users = dbContext.Users;
                foreach (var user in users)
                {
                    if (user.UserName == login && user.UserPassword == password)
                    {
                        _role = user.UserRole;
                        _login = user.UserName;
                        _password = user.UserPassword;
                        _succes = true;
                    }
                }
            }
            AddTask();
            RoleInfoInWindow();
        }

        public void Profile_Click(object sender, RoutedEventArgs e)
        {
            if (_succes)
            {
                string fullName = null;
                string lessonOnGroup = null;
                using (var dbContext = new MyDBContext())
                {
                    if (_role == "student")
                    {
                        var students = dbContext.Students;
                        foreach (var student in students)
                        {
                            if (student.StudentUserName == _login)
                            {
                                fullName = student.StudentFullName;
                                lessonOnGroup = student.StudentGroup;
                            }
                        }
                    }

                    else if (_role == "teacher")
                    {
                        var teachers = dbContext.Teachers;
                        foreach (var teacher in teachers)
                        {
                            if (teacher.TeacherUserName == _login)
                            {
                                fullName = teacher.TeacherFullName;
                                lessonOnGroup = teacher.LessonName;
                            }
                        }
                    }
                }
                Profile profile = new Profile(fullName, lessonOnGroup, _role, _login, _password);
                profile.Show();
                Close();

            }

            else
                MessageBox.Show("Аккаунт не зарегестрирован");
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            AutorizationWindow autorizationWindow = new AutorizationWindow();
            autorizationWindow.Show();
            Close();

        }

        void AddTask()
        {
            if (_role == "teacher")
            {
                using (var dbContext = new MyDBContext())
                {
                    var teachers = dbContext.Teachers;
                    foreach (var teacher in teachers)
                    {

                        if (_password == teacher.TeacherPassword && _login == teacher.TeacherUserName)
                            _lessonNameOrGroup = teacher.LessonName;
                    }

                    var tasks = dbContext.Tasks;
                    foreach (var task in tasks)
                    {
                        if (task.LessonName == _lessonNameOrGroup)
                        {
                            TasksListBox.Items.Add(task.Name);
                        }
                    }
                }
            }

            else if (_role == "student")
            {
                using (var dbContext = new MyDBContext())
                {
                    var students = dbContext.Students;
                    foreach (var student in students)
                    {
                        if (_password == student.StudentPassword && _login == student.StudentUserName)
                            _lessonNameOrGroup = student.StudentGroup;

                    }

                    var tasks = dbContext.Tasks;
                    foreach (var task in tasks)
                    {
                        if (task.Group == _lessonNameOrGroup)
                        {
                            TasksListBox.Items.Add(task.Name);
                        }
                    }
                }

            }
        }

        private void ReadyTask_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = TasksListBox.SelectedItem;
            if (selectedItem != null)
            {
                string content = selectedItem.ToString();
                if (selectedItem != null && selectedItem is string)
                {
                    ListBoxItem selectedListBoxItem = TasksListBox.ItemContainerGenerator.ContainerFromItem(selectedItem) as ListBoxItem;
                    if (selectedListBoxItem != null)
                    {
                        selectedListBoxItem.Foreground = Brushes.Green;
                        using (var dbContext = new MyDBContext())
                        {
                            var tasks = dbContext.Tasks;
                            foreach (var task in tasks)
                            {
                                if (task.Name == content)
                                    task.IsReady = true;
                            }
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
        }

        private void AddTaskButtoon_Click(object sender, RoutedEventArgs e)
        {
            if (_role == "teacher")
            {
                AddTaskWindow addTaskWindow = new AddTaskWindow(_login, _password);
                addTaskWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Недостаточно прав");
            }
        }

        private void TasksListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object selectedItem = TasksListBox.SelectedItem;
            if (selectedItem != null)
            {
                string taskName = selectedItem.ToString();
                int taskID = 0;
                using (var dbContext = new MyDBContext())
                {
                    var tasks = dbContext.Tasks;
                    foreach (var task in tasks)
                    {
                        if (task.Name == taskName)
                        {
                            taskID = task.TaskID;
                            string ready;
                            if (task.IsReady)
                                ready = "Выполнено";
                            else
                                ready = "Не выполнено";
                            TaskInfo.Text = "Дата создания: " + task.DateAddTask + "\n" + "Название прдемета: " + task.LessonName + "\n" + "Номер группы: " + task.Group + "\n" + ready;
                        }
                    }
                    var taskDescriptions = dbContext.TaskDescriptions;
                    foreach (var taskDescription in taskDescriptions)
                    {
                        if (taskDescription.TaskId == taskID)
                        {
                            TackDescription.Text = taskDescription.Description;
                        }
                    }
                }
            }
        }

        private void DeleteTaskButtoon_Click(object sender, RoutedEventArgs e)
        {
            if (_role == "teacher")
            {
                object selectedItem = TasksListBox.SelectedItem;
                if (selectedItem != null)
                {
                    string content = selectedItem.ToString();
                    using (var dbContext = new MyDBContext())
                    {
                        dbContext.Database.CreateIfNotExists();
                        var tasks = dbContext.Tasks;
                        bool recuringName = false;
                        int taskIDToDelete = 0;
                        foreach (var task in tasks)
                        {
                            if (task.Name == content && task.IsReady == true)
                            {
                                var itemToDelete = dbContext.Tasks.Find(task.TaskID);
                                taskIDToDelete = task.TaskID;
                                dbContext.Tasks.Remove(itemToDelete);
                                TasksListBox.Items.Remove(selectedItem);

                                // Сохраняем изменения в базе данных

                            }
                        }

                        if (taskIDToDelete >= 0)
                        {
                            var taskDescriptions = dbContext.TaskDescriptions;
                            foreach (var taskDescription in taskDescriptions)
                            {
                                if (taskDescription.TaskId == taskIDToDelete)
                                {
                                    var taskDescriptionToDelete = dbContext.TaskDescriptions.Find(taskDescription.TaskId);
                                    dbContext.TaskDescriptions.Remove(taskDescriptionToDelete);
                                }
                            }
                        }
                        else
                            MessageBox.Show("Задача не выполнена" + '\n' + "Удаление не возможно");
                        dbContext.SaveChanges();
                    }
                }
            }

            else
            {
                MessageBox.Show("Недостаточно прав");
            }
        }
    }
}
