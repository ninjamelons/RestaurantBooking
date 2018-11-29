
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ControllerLibrary;
using ModelLibrary;
using RestaurantDesktopClient.ItemService;
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
            var proxy = new MenuServiceClient();
            var modelMenu = proxy.GetAllMenusByRestaurant(1000000);
            foreach (ModelLibrary.Menu item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
            InitializeComponent();
            labelRestaurantId.Content = 1000000;
        }
        
        private IEnumerable<ModelLibrary.Menu> GetMenus(int restaurantId)
        {
            var proxy = new MenuServiceClient();
            return proxy.GetAllMenusByRestaurant(restaurantId);
        }
        /*private void menuNameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridItemList.Items.Clear();
            // put restaurant id here when possible
            //dataGridItemList.Items.Add = itemCtrl.GetMenuItems(modelMenu.Id);
            //itemTable.RowGroups.Add(new TableRowGroup());
            //itemTable.RowGroups[itemTable.RowGroups.Count].Rows.Add(new TableRow());
            
            var proxy = new MenuServiceClient();
            var proxy1 = new ItemServiceClient();
            
            var selectedMenu = (ModelLibrary.Menu)menuNameBox.SelectedItem;
            var modelMenu = proxy.GetMenu(selectedMenu);
            textBoxName.Text = modelMenu.Name;
            hiddenName.Content = modelMenu.Name;
            
            checkBoxActive.IsChecked = modelMenu.Active;
            
            foreach (Menu item in modelMenu)
            {
                /*string[] itemo = new string[3];
                itemo[0] = item.Name;
                itemo[1] = item.Description;
                itemo[2] = proxy1.GetItemPrice(item, 1000000).ToString();
                itemo[2] = item.ItemCat.ToString();
                dataGridItemList.Items.Add(item);
               
            };
            

    
        }*/
        /*private void buttonSaveName_Click(object sender, RoutedEventArgs e)
        {
            MenuCtrl menuCtrl = new MenuCtrl();
            var selectedMenu = (ModelLibrary.Menu)menuNameBox.SelectedItem;                      ModelLibrary.Menu selectMenu = dataGridItemList.SelectedItem as ModelLibrary.Menu;
                                                                                                 int restaurantId = selectMenu.Id;
            var proxy = new MenuServiceClient(); 
            var oldMenu = new ModelLibrary.Menu
            {
                Id = selectedMenu.Id,
                Name = selectedMenu.Name,
                Active = selectedMenu.Active,
                Items = selectedMenu.Items

            };
            bool newBool = (bool)checkBoxActive.IsChecked;
            var newMenu = new ModelLibrary.Menu
            {
                Id = selectedMenu.Id,
                Name = textBoxName.Text,
                Active = newBool,
                Items = selectedMenu.Items
            };
            proxy.UpdateMenu(oldMenu, newMenu, 1000000);
            MessageBox.Show("Menu updated!");
        }*/

        private void buttonAdd_Click(object sender, RoutedEventArgs e) // save menu
        {
            

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedMenu = (ModelLibrary.Menu)dataGridItemList.SelectedItem;
            //labelRestaurantId = Convert.ToInt32()  
            var proxy = new MenuServiceClient();
            proxy.DeleteMenu(selectedMenu);
            dataGridItemList.Items.Clear();
            var modelMenu = proxy.GetAllMenusByRestaurant(1000000);
            foreach (ModelLibrary.Menu item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };


        }

        private void buttonCreateMenu_Click(object sender, RoutedEventArgs e)
        {
            bool? checkBox = checkBoxActive.IsChecked;
            bool checkBoxBool = checkBox ?? false;
            var proxy = new MenuServiceClient();
            ModelLibrary.Menu modelMenu = new ModelLibrary.Menu
            {
                RestaurantId = 1000000,
                Name = textBoxName.Text,
                Items = null,
                Active = checkBoxBool
                
            };
            proxy.CreateMenu(modelMenu);
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSaveName_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
