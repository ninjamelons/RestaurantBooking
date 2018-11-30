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

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for RestaurantHome.xaml
    /// </summary>
    public partial class RestaurantHome : Page
    {
        public RestaurantHome()
        {
            InitializeComponent();
        }

        private void ToTablesPage_OnClick(object sender, RoutedEventArgs e)
        {
            // View TablesCrud Page
            TablesCrud tablesPage = new TablesCrud();
            this.NavigationService.Navigate(tablesPage);
        }

        private void ManageMenus_Click(object sender, RoutedEventArgs e)
        {
            //View MenusCrud Page
            MenusCrud menuPage = new MenusCrud();
            this.NavigationService.Navigate(menuPage);
        }

        private void buttonItemCat_Click(object sender, RoutedEventArgs e)
        {
            ItemCatCrud itemPage = new ItemCatCrud();
            this.NavigationService.Navigate(itemPage);
        }

        private void buttonItemCat_Click_1(object sender, RoutedEventArgs e)
        {
            ItemCatCrud itemPage = new ItemCatCrud();
            this.NavigationService.Navigate(itemPage);
            
        }

        private void buttonItem_Click(object sender, RoutedEventArgs e)
        {
            ItemCrud itemPage = new ItemCrud();
            this.NavigationService.Navigate(itemPage);
        }
    }
}
