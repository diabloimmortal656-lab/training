using perfume.Model;
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

namespace perfume.Vews
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();

            try
            {
                App.DBCon = new DBCon();
                MessageBox.Show("Cоединения с БД успешно", "Подключение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка соединения с БД",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            MessageBox.Show("Добро пожаловать Гость", "Добро пожаловать", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new Catalog());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = App.DBCon
                .User
                .Where(u => u.Login == tbLogin.Text && u.Password == tbPassword.Text).FirstOrDefault();
            if (App.CurrentUser != null)
            {
                var user = App.CurrentUser;
                MessageBox.Show($"Добро пожаловать {user.FullName} ваша роль {user.Role.NameRole}", "Добро пожаловать", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new Catalog());
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }
    }
}
