
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ControllerLibrary;
using ModelLibrary;
using RestaurantDesktopClient.MenuService;


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
            menuNameBox.ItemsSource = GetMenus(1000000);
            labelRestaurantId.Content = 1000000;
        }
        
        private IEnumerable<ModelLibrary.Menu> GetMenus(int restaurantId)
        {
            var proxy = new MenuServiceClient();
            return proxy.GetAllMenusByRestaurant(restaurantId);
        }
        private void menuNameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridItemList.Items.Clear();

            ItemCtrl itemCtrl = new ItemCtrl();
            var proxy = new MenuServiceClient();
            var selectedMenu = (ModelLibrary.Menu)menuNameBox.SelectedItem;
            var modelMenu = proxy.GetMenu(selectedMenu);
            textBoxName.Text = modelMenu.Name;
            hiddenName.Content = modelMenu.Name;
            // put restaurant id here when possible
            checkBoxActive.IsChecked = modelMenu.Active;
            //dataGridItemList.Items.Add = itemCtrl.GetMenuItems(modelMenu.Id);
            //itemTable.RowGroups.Add(new TableRowGroup());
            //itemTable.RowGroups[itemTable.RowGroups.Count].Rows.Add(new TableRow());
            
            foreach (Item item in modelMenu.Items)
            {
                dataGridItemList.Items.Add(item);
                
            };
            


        }


        private void buttonAdd_Click(object sender, RoutedEventArgs e) // save menu
        {
            MenuCtrl menuCtrl = new MenuCtrl();
            var selectedMenu = (ModelLibrary.Menu)menuNameBox.SelectedItem;
            var proxy = new MenuServiceClient();
            var oldMenu = new ModelLibrary.Menu
            {
                Id = selectedMenu.Id,
                Name = selectedMenu.Name,
                Active = selectedMenu.Active,
                Items = selectedMenu.Items
                 
            };
            bool newBool = checkBoxActive.IsChecked ?? false;
            var newMenu = new ModelLibrary.Menu
            {
                Id = selectedMenu.Id,
                Name = textBoxName.Text,
                Active = newBool,
                Items = selectedMenu.Items
            };
            proxy.UpdateMenu(oldMenu, newMenu, 1000000);
            MessageBox.Show("Menu updated!");

        }
        

        
    }
}
