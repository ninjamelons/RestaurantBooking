
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
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
            //listBoxItems.Items.Add = me
            //menuNameBox.ItemsSource = GetMenus();
        }

        

        private void MenuNameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proxy = new MenuServiceClient();
            var selectedMenu = (ModelLibrary.Menu)menuNameBox.SelectedItem;
            var dbMenu = proxy.GetMenu(selectedMenu);
            textBoxName.Text = dbMenu.Name;
            hiddenName.Content = dbMenu.Name;
            checkBoxActive.IsChecked = dbMenu.Active;
            foreach (Item item in dbMenu.Items)
                    {
                      listBoxItems.Items.Add(item);
                    }
            
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var proxy = new MenuServiceClient();
            var oldMenu = new ModelLibrary.Menu
            {
                Name = ""
            };

        }
        

        
    }
}
