using RestaurantDesktopClient.RestaurantService;
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
            dataGrid.ItemsSource = GetTables();
        }
        public int resId;
        //public event EventHandler<MyEventArgs> SomethingChanged;
        public IEnumerable<ModelLibrary.Table> GetTables()
        {
            var proxy = new RestaurantServiceClient();
            return proxy.GetTablesWithReserved(resId);
        }
        
        private void ToTablesPage_OnClick(object sender, RoutedEventArgs e)
        {
            // View TablesCrud Page
            var tablesPage = new TablesCrud(resId);
            this.NavigationService.Navigate(tablesPage);
        }

        private void ManageMenus_Click(object sender, RoutedEventArgs e)
        {
            //View MenusCrud Page
            MenusCrud menuPage = new MenusCrud(resId);
            this.NavigationService.Navigate(menuPage);
        }

        private void buttonRestaurant_Click(object sender, RoutedEventArgs e)
        {
            RestaurantUpdate resPage = new RestaurantUpdate(resId);
            this.NavigationService.Navigate(resPage);
        }

        
        private void OnOnChecked(object sender, RoutedEventArgs e)
        {
            var selectedTable = (ModelLibrary.Table)dataGrid.SelectedItem;
            var proxy = new RestaurantServiceClient();
            MessageBoxResult result = MessageBox.Show("Do you really want to reserve this table?","Confirmation", MessageBoxButton.YesNo);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    proxy.ReserveSingleTable(selectedTable.TableId, resId);
                    dataGrid.ItemsSource = null;
                    dataGrid.Items.Refresh();
                    dataGrid.ItemsSource = GetTables();
                    break;
                case MessageBoxResult.No:
                    dataGrid.ItemsSource = null;
                    dataGrid.Items.Refresh();
                    dataGrid.ItemsSource = GetTables();
                    break;
            }
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dataGrid.SelectedItem
        }
    }
}
