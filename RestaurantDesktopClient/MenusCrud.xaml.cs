
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
        
        public MenusCrud(int resId)
        {
            restaurantId = resId;
            InitializeComponent();
            dataGridItemList.SelectedItem = Services._MenuProxy.GetMenuById(1000000);
            var modelMenu = Services._MenuProxy.GetAllMenusByRestaurant(restaurantId);
            foreach (ModelLibrary.Menu item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
            InitializeComponent();
            labelRestaurantId.Content = restaurantId;
        }
        int restaurantId;
        private IEnumerable<ModelLibrary.Menu> GetMenus(int restaurantId)
        {
            var proxy = new MenuServiceClient();
            return proxy.GetAllMenusByRestaurant(restaurantId);
        }
       

        private void buttonAdd_Click(object sender, RoutedEventArgs e) // save menu
        {
            

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedMenu = (ModelLibrary.Menu)dataGridItemList.SelectedItem;
            if (selectedMenu == null)
            {
                MessageBox.Show("Nothing selected");
            }
            else
            {
                //labelRestaurantId = Convert.ToInt32()  
                await Services._MenuProxy.DeleteMenuAsync(selectedMenu.Id);
                dataGridItemList.Items.Clear();
                var modelMenu = await Services._MenuProxy.GetAllMenusByRestaurantAsync(restaurantId);
                foreach (ModelLibrary.Menu item in modelMenu)
                {
                    dataGridItemList.Items.Add(item);
                };
            }

        }

        private async void buttonCreateMenu_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length < 2)
            {
                MessageBox.Show("InvalidName");
            }
            else
            {
                bool newBool = checkBoxActive.IsChecked ?? false;
                ModelLibrary.Menu modelMenu = new ModelLibrary.Menu
                {
                    RestaurantId = restaurantId,
                    Name = textBoxName.Text,
                    Items = null,
                    Active = newBool //////////////////////////CHECK BOXXXXXXXXXXXXXXXXXXXXXXXXXX

                };

                await Services._MenuProxy.CreateMenuAsync(modelMenu);
                dataGridItemList.Items.Clear();
                var modelMenu1 = await Services._MenuProxy.GetAllMenusByRestaurantAsync(restaurantId);
                foreach (ModelLibrary.Menu item in modelMenu1)
                {
                    dataGridItemList.Items.Add(item);
                };

            }
        }

        private async void buttonSaveName_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ModelLibrary.Menu)dataGridItemList.SelectedItem;
            bool newBool = checkBoxActive.IsChecked ?? false;
            if (textBoxName.Text.Length < 2)
            {
                MessageBox.Show("NameInvalid");
            }
            else 
            {
                var oldMenu = new ModelLibrary.Menu
                {
                    Id = selectedItem.Id,
                    Active = selectedItem.Active,
                    Items = selectedItem.Items,
                    Name = selectedItem.Name,
                    RestaurantId = restaurantId

                };
                var newMenu = new ModelLibrary.Menu
                {
                    Id = selectedItem.Id,
                    Items = selectedItem.Items,
                    Active = newBool,
                    Name = textBoxName.Text,
                    RestaurantId = restaurantId
                };
                await Services._MenuProxy.UpdateMenuAsync(oldMenu, newMenu);
                dataGridItemList.Items.Clear();
                var modelMenu = await Services._MenuProxy.GetAllMenusByRestaurantAsync(restaurantId);
                foreach (ModelLibrary.Menu item in modelMenu)
                {
                    dataGridItemList.Items.Add(item);
                };
            }
        }

        private void dataGridItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dataGridItemList.SelectedItem as ModelLibrary.Menu;
            if (selectedItem == null)
            {
                textBoxName.Text = "";
            }
            else
            {
                textBoxName.Text = dataGridItemList.SelectedItem.ToString();
                checkBoxActive.IsChecked = selectedItem.Active;
            }
        }

        private void buttonItems_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedItem = (ModelLibrary.Menu)dataGridItemList.SelectedItem;
            if (selectedItem != null)
            {
                ItemCrud tablesPage = new ItemCrud(selectedItem.Id, restaurantId);
                this.NavigationService.Navigate(tablesPage);
            }
            else
            {
                MessageBox.Show("select an Item First");
            }
           
           
        }

        private void buttonItemCats_Click(object sender, RoutedEventArgs e)
        {

            ItemCatCrud tablesPage = new ItemCatCrud();
            this.NavigationService.Navigate(tablesPage);
        }
    }
}
