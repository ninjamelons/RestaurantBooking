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
        
        public RestaurantHome(int restaurantId)
        {
            resId = restaurantId;
            InitializeComponent();
        }
        public int resId;
        private void ToTablesPage_OnClick(object sender, RoutedEventArgs e)
        {
            // View TablesCrud Page
            TablesCrud tablesPage = new TablesCrud();
            this.NavigationService.Navigate(tablesPage);
        }

        private void ManageMenus_Click(object sender, RoutedEventArgs e)
        {
            //View MenusCrud Page
            MenusCrud menuPage = new MenusCrud(resId);
            this.NavigationService.Navigate(menuPage);
        }
    }
}
