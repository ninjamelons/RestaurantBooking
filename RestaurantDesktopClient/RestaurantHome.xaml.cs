using RestaurantDesktopClient.OrderService;
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
            dataGridOrder.ItemsSource = GetOrders();
        }
        public int resId;
        //public event EventHandler<MyEventArgs> SomethingChanged;
        public IEnumerable<ModelLibrary.Table> GetTables()
        {
            return Services._RestaurantProxy.GetAllTablesByRestaurant(resId);
        }
        
        public IEnumerable<ModelLibrary.Order> GetOrders()
        {
            return Services._OrderProxy.GetAllOrdersByRestaurant(resId);
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
            var orderGrid = dataGridOrder.SelectedItem;
            var selectedTable = (ModelLibrary.Table)dataGrid.SelectedItem;
            //var proxy = new RestaurantServiceClient();
            MessageBoxResult result = MessageBox.Show("Do you really want to reserve this table?","Confirmation", MessageBoxButton.YesNo);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    string message = Services._RestaurantProxy.ReserveSingleTable(selectedTable.TableId, resId);
                    Services._RestaurantProxy.ReserveSingleTable(selectedTable.TableId, resId);
                    dataGrid.ItemsSource = null;
                    dataGrid.Items.Refresh();
                    dataGrid.ItemsSource = GetTables();
                    MessageBox.Show(message);
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
        
        private void DataGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridOrder_DoubleClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (ModelLibrary.Order)dataGridOrder.SelectedItem;
            var returnedOrder = Services._OrderProxy.GetOrderById(int.Parse(selectedOrder.OrderId));
            var itemLists = new List<ModelLibrary.OrderLineItem>();
            var price = returnedOrder.TotalPriceCent / 100;
            foreach(var a in returnedOrder.ItemsList)
            {
                itemLists.Add(a);
            };
            string message = "Number Of seats " + returnedOrder.NoSeats.ToString() + "Price " + returnedOrder.TotalPriceCent.ToString()
                             + "Date " + returnedOrder.ReservationDateTime.ToString();
            MessageBox.Show(message);
        }

        private void ButtonDecline_Click(object sender, RoutedEventArgs e)
        {
            //var proxy = new OrderServiceClient();
            var selectedItem = (ModelLibrary.Order)dataGridOrder.SelectedItem;
            var selectedOrder = Services._OrderProxy.GetOrderById(int.Parse(selectedItem.OrderId));
            var newOrder = new ModelLibrary.Order
            {
                RestaurantId = selectedOrder.RestaurantId,
                CustomerId = selectedOrder.CustomerId,
                DateTime = selectedOrder.DateTime,
                ItemsList = selectedOrder.ItemsList,
                NoSeats = selectedOrder.NoSeats,
                OrderId = selectedOrder.OrderId,
                Payment = selectedOrder.Payment,
                ReservationDateTime = selectedOrder.ReservationDateTime,
                Accepted = false,

            };
            Services._OrderProxy.UpdateOrder(newOrder);
            dataGridOrder.ItemsSource = null;
            dataGridOrder.ItemsSource = GetOrders();
            MessageBox.Show("Order Declined");

        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            //var proxy = new OrderServiceClient();
            var selectedItem = (ModelLibrary.Order)dataGridOrder.SelectedItem;
            var selectedOrder = Services._OrderProxy.GetOrderById(int.Parse(selectedItem.OrderId));
            var newOrder = new ModelLibrary.Order
            {
                RestaurantId = selectedOrder.RestaurantId,
                CustomerId = selectedOrder.CustomerId,
                DateTime = selectedOrder.DateTime,
                ItemsList = selectedOrder.ItemsList,
                NoSeats = selectedOrder.NoSeats,
                OrderId = selectedOrder.OrderId,
                Payment = selectedOrder.Payment,
                ReservationDateTime = selectedOrder.ReservationDateTime,
                Accepted = true,

            };
            Services._OrderProxy.UpdateOrder(newOrder);
            dataGridOrder.ItemsSource = null;
            dataGridOrder.ItemsSource = GetOrders();
            MessageBox.Show("Order Accepted");
        }
    }
}
