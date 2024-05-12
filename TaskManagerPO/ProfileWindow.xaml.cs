using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace TaskManagerPO
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    
    public partial class Profile : Window
    {
        public Profile(string fullName, string groupOrLesson, string role, string login, string password)
        {
            InitializeComponent();
            _fullName = fullName;
            _groupOrLessonName = groupOrLesson;
            _role = role;
            _login = login;
            _password = password;
            TextBoxData();
            PhotoProfile();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        
        string _fullName;
        string _groupOrLessonName;
        string _role;
        string _login;
        string _password;
        
        void TextBoxData()
        {
            NameDataTextBox.Text = _fullName;
            GroupOrLessonDataTextBox.Text = _groupOrLessonName;
            if (_role == "teacher")
                GroupOrLessonTextBox.Text = "Предмет";
            else if (_role =="student")
                GroupOrLessonTextBox.Text = "Группа";
        }

        void PhotoProfile()
        {
            ProfileImage.Source = null;
            using (var dbContext = new MyDBContext())
            {
                if (_role == "student")
                {
                    var students = dbContext.Students;
                    foreach (var student in students)
                    {
                        if (student.StudentUserName == _login && student.StudentPassword == _password)
                        {
                            if(student.ImageData != null)
                            {
                                byte[] decodedBytes = student.ImageData;
                                // Создание изображения из массива байтов
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = new MemoryStream(decodedBytes);
                                bitmapImage.EndInit();

                                // Теперь у вас есть изображение, созданное из строки байтов, которое вы можете использовать по вашему усмотрению
                                ProfileImage.Source = bitmapImage;
                            }
                            
                        }

                    }
                }

                else if (_role == "teacher")
                {
                    var teachers = dbContext.Teachers;
                    foreach (var teacher in teachers)
                    {
                        if (teacher.TeacherUserName == _login && teacher.TeacherPassword == _password)
                        {
                            if(teacher.ImageData != null)
                            {
                                byte[] decodedBytes = teacher.ImageData;
                                // Создание изображения из массива байтов
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = new MemoryStream(decodedBytes);
                                bitmapImage.EndInit();

                                // Теперь у вас есть изображение, созданное из строки байтов, которое вы можете использовать по вашему усмотрению
                                ProfileImage.Source = bitmapImage;
                            }
                            
                        }

                    }
                }
            }
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(selectedImagePath);
                
                using (var dbContext = new MyDBContext())
                {
                    if (_role == "student")
                    {
                        var students = dbContext.Students;
                        foreach (var student in students)
                        {
                            if (student.StudentUserName == _login && student.StudentPassword == _password)
                            {
                                MessageBox.Show("12");
                                student.ImageData = imageBytes;
                                
                                MessageBox.Show("21");
                            }

                        }
                    }

                    else if (_role == "teacher")
                    {
                        var teachers = dbContext.Teachers;
                        foreach (var teacher in teachers)
                        {
                            if (teacher.TeacherUserName == _login && teacher.TeacherPassword == _password)
                            {
                                teacher.ImageData = imageBytes;
                            }
                        }
                    }
                    dbContext.SaveChanges();
                }
                ProfileImage.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TaskManagerWindow taskManagerWindow = new TaskManagerWindow();
            taskManagerWindow.User(_login, _password);
            taskManagerWindow.Show();
            Close();
        }

        private void SaveDataUser_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new MyDBContext())
            {
                if (_role == "student")
                {
                    var students = dbContext.Students;
                    foreach (var student in students)
                    {
                        if (student.StudentUserName == _login && student.StudentPassword == _password)
                        {
                            string[] fullNameStudentArray = NameDataTextBox.Text.Split(' ');
                            if (NameDataTextBox.Text.Length >= 0 && fullNameStudentArray.Length == 2)
                                student.StudentFullName = NameDataTextBox.Text;
                            else
                                MessageBox.Show("Введеное ФИО не соответвует формату");

                            string pattern = @"^\d[а-я]{1,2}\d$";
                            string groupStudent = GroupOrLessonDataTextBox.Text;
                            bool isValid = Regex.IsMatch(groupStudent, pattern);
                            if (isValid)
                                student.StudentGroup = GroupOrLessonDataTextBox.Text;
                            else
                                MessageBox.Show("Неверно введен формат номера группа");
                        }
                    }
                }

                else if (_role == "teacher")
                {
                    var teachers = dbContext.Teachers;
                    foreach (var teacher in teachers)
                    {
                        if (teacher.TeacherUserName == _login && teacher.TeacherPassword == _password)
                        {
                            string[] fullNameStudentArray = NameDataTextBox.Text.Split(' ');
                            if (NameDataTextBox.Text.Length >= 0 && fullNameStudentArray.Length == 2)
                                teacher.TeacherFullName = NameDataTextBox.Text;
                            else
                                MessageBox.Show("Введеное ФИО не соответвует формату");

                            if (GroupOrLessonDataTextBox.Text != null)
                                teacher.LessonName = GroupOrLessonDataTextBox.Text;
                            else
                                MessageBox.Show("Поле названия предмета не может быть пустым");
                        }

                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
