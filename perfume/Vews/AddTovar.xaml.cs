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
    /// Логика взаимодействия для AddTovar.xaml
    /// </summary>
    public partial class AddTovar : Page
    {
        
        public int mode;
        public AddTovar(int mode, Model.Tovar tovar)
        {
            InitializeComponent();
            var provider = App.DBCon.Provider.ToList();
            cbProvider.ItemsSource = provider;

            var category = App.DBCon.Category.ToList();
            cbNameCategory.ItemsSource = category;
            if(mode == 1)
                _AddTovar();
            if (mode == 2)
                _EditTovar();
        }

        private void _AddTovar()
        {
            
        }
        private void _EditTovar()
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
