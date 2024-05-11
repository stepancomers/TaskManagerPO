using System.Windows;

namespace TaskManagerPO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 

    //добавить обработку повторяющихся логинов(для всех)
    //добавить обработчик ошибок для всех
    //добавить загрузку фото
    //добавить листбокс задач
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AutorizationWindowOpen();
        }

        void AutorizationWindowOpen()
        {
            AutorizationWindow autorization = new AutorizationWindow();
            autorization.Show();
            using (var dbContext = new MyDBContext())
            {
                dbContext.Database.CreateIfNotExists();
            }
            Close();

        }
    }
}
