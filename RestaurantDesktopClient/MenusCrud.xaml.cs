
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using ModelLibrary;
using RestaurantDesktopClient.RestaurantService;
using RestaurantService;

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for MenusCrud.xaml
    /// </summary>
    public partial class MenusCrud : Page
    {
        public MenusCrud()
        {
            InitializeComponent();
            hiddenMenuId.Content = "0";
            MenuService menuService = new MenuService();
            //listBoxItems.Items.Add = me
            //menuNameBox.ItemsSource = GetMenus();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuNameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var selectedMenu = (ModelLibrary.Menu)menuNameBox.SelectedItem;
            var dbMenu = proxy.GetMenu(selectedMenu);
            textBoxName.Text = dbMenu.Name;
            checkBoxActive.IsChecked = dbMenu.Active;


        }
        /*private List<DatabaseAccessLibrary.Item> getItems(int menuId)
{
   ItemDb itemDb = new ItemDb();
   itemDb.GetItems
   return;
}*/

        /*public IEnumerable<Menu> GetMenus()
        {
            MenuService menuService = new MenuService();
            return menuService.GetAllMenusByRestaurant(Convert.ToInt32(hiddenMenuId));
            
        }*/
    }
}
