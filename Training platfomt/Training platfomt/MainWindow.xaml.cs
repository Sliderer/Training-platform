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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Training_platfomt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseController database;
        private User currentUser;
        private Grid lastUsedGrid;
        private static MainWindow mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            database = new DatabaseController();
            mainWindow = this;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimazeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizedButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void ToolBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuBarColumn.Width == new GridLength( MenuBarColumn.MinWidth))
            {
                MenuBarColumn.Width = new GridLength(MenuBarColumn.MaxWidth);
            }
            else
            {
                MenuBarColumn.Width = new GridLength(MenuBarColumn.MinWidth);
            }
        }

        private void ChangeEnterGrid(object sender, RoutedEventArgs e)
        {
            if (LoginGrid.Visibility == Visibility.Hidden)
            {
                LoginGrid.Visibility = Visibility.Visible;
                RegistrationGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                LoginGrid.Visibility = Visibility.Hidden;
                RegistrationGrid.Visibility = Visibility.Visible;
            }
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameRegistrationGridTextBox.Text;
            string surname = SurnameRegistrationGridTextBox.Text; 
            string login = LoginRegistrationGridTextBox.Text;
            string email = EmailRegistrationGridTextBox.Text;
            string password = PasswordRegistrationGridTextBox.Text;

            if (database.FindUser(login) != null)
            {
                return;
            }

            User user = new User(name, surname, login, email, password.GetHash());
            database.AddUser(user);
            currentUser = user;
            EnterAccount();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginLoginTextBox.Text;
            string password = LoginPasswordTextBox.Text.GetHash();

            User user = database.FindUser(login);
            if (user != null)
            {
                if (user.password == password)
                {
                    currentUser = user;
                    EnterAccount();
                }
                else
                {
                    MessageBox.Show("Неверный паролль");
                }
            }
            else
            {
                MessageBox.Show("Пользователя не существует!");
            }
        }

        private void EnterAccount()
        {
            EnterGrid.Visibility = Visibility.Hidden;
            AccountGrid.Visibility = Visibility.Visible;
            FillInfoController.FillUserInfo(currentUser, this);
            FillInfoController.FillCoursesWrapPanel(database, this);
        }

        private void CourseStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lastUsedGrid != CoursesGrid)
            {
                ChangeGridinAccount(CoursesGrid);
            }
        }

        private void AccountStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lastUsedGrid != ProfileGrid)
            {
                ChangeGridinAccount(ProfileGrid);
            }
        }

        public static void CoursePanel_ChooseCourseEvent(object sender, RoutedEventArgs e)
        {
            string title = (sender as CoursePanel).CourseTitle.Text;
            Course chosenCourse = mainWindow.database.GetCourse(title);
            List<Video> videos = mainWindow.database.GetCourseVideos(chosenCourse.id).ToList();
            FillInfoController.FillCourseGrid(chosenCourse, videos, mainWindow.CurrentCourseGrid, mainWindow);
        }

        internal void ChangeGridinAccount(Grid grid)
        {
            if (lastUsedGrid != null)
            {
                lastUsedGrid.Visibility = Visibility.Hidden;
            }
            grid.Visibility = Visibility.Visible;

            lastUsedGrid = grid;
        }
    }
}
