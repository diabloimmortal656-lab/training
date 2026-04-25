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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace perfume.Vews
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    /// 
    
    public partial class Catalog : Page
    {
        public Catalog()
        {
            InitializeComponent();
            var window = Application.Current.MainWindow;
            window.Width = 1200;
            window.Height = 800;

            Provider provider = new Provider
            {
                NameProvider = "Все поставщики",
                IDNameProvider = 0
            };

            tbUser.Text = App.CurrentUser != null ? App.CurrentUser.FullName : "Гость";

            var providers = App.DBCon.Provider.ToList();
            providers.Insert(0, provider);
            cbProvider.ItemsSource = providers;
            if(App.CurrentUser != null)
            {
                var role = App.CurrentUser.Role.IDNameRole;
                if (role < 3)
                {
                    GridSearch.Visibility = Visibility.Visible;
                }
                if (role < 2)
                {
                    footter.Visibility = Visibility.Visible;
                }
            }
            main();
        }

        private void main ()
        {
            var tovar = App.DBCon.Tovar.ToList();
            lbTovar.ItemsSource = tovar;

            var search = tovar.AsEnumerable();
            if(!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                string text = tbSearch.Text.ToLower();

                lbTovar.ItemsSource = search = search.Where(s =>
                (s.NameTovar.TovarName ?? "").ToLower().Contains(text) ||
                (s.Category.NameCategory ?? "").ToLower().Contains(text) ||
                (s.Manufacture.NameManufacture ?? "").ToLower().Contains(text) ||
                (s.Description ?? "").ToLower().Contains(text)
                );
            }

            if (cbProvider.SelectedIndex > 0)
            {
                lbTovar.ItemsSource = search = search.Where(p => p.IDProvider == cbProvider.SelectedIndex);
            }

            if (rbUp.IsChecked == true)
            {
                lbTovar.ItemsSource = search = search.OrderBy(x => x.Count).ToList();
            }
            if (rbDown.IsChecked == true)
            {
                lbTovar.ItemsSource = search = search.OrderByDescending(x => x.Count).ToList();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;

            window.Width = 800;
            window.Height = 450;

            NavigationService.GoBack();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e) => main();

        private void cbProvider_SelectionChanged(object sender, SelectionChangedEventArgs e) => main();

        private void rbUp_Checked(object sender, RoutedEventArgs e) => main();

        private void rbDown_Checked(object sender, RoutedEventArgs e) => main();
    }
}
