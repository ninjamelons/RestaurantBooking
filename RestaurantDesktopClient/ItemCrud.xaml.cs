using ControllerLibrary;
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
using ModelLibrary;
using RestaurantDesktopClient.ItemService;
using RestaurantDesktopClient.MenuService;
using RestaurantDesktopClient.PriceService;

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for ItemCrud.xaml
    /// </summary>
    public partial class ItemCrud : Page
    {
        public ItemCrud(ModelLibrary.Menu menu)
        {
            InitializeComponent();
            var itemProxy = new ItemServiceClient();
            comboBoxCategory.ItemsSource = itemProxy.GetAllItemCategories();
            var selectedMenu = new ModelLibrary.Menu
            {
                Active = menu.Active,
                Name = menu.Name,
                Id = menu.Id,
                Items = menu.Items,
                RestaurantId = menu.RestaurantId
            };
            if(menu != null)
            {
                var menuName = menu.Name;
                //labelMenuName.Content = menu;
               // labelMenuId.Content = 1000000; //menu.Id;
            }
            

           
            var proxyMenu = new MenuServiceClient();
            var proxy = new ItemServiceClient();
            //comboBoxMenuId.ItemsSource = proxyMenu.GetAllMenusByRestaurant(1000000);
            var modelMenu = proxy.GetAllItemsByMenu(selectedMenu.Id);
            foreach (Item item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };

        }
        
        private IEnumerable<ModelLibrary.ItemCat> GetItemCats()
        {
            var proxy = new ItemServiceClient();
            
            return proxy.GetAllItemCategories();
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var random = comboBoxCategory.SelectedItem;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            var proxyPrice = new PriceServiceClient();
            var proxy = new ItemServiceClient();
            double price;
            bool success = double.TryParse(textBoxNamePrice.Text, out price);
            if (success) {
                var newPrice = new ModelLibrary.Price
                {
                    VarPrice = price

                };
                var newItem = new ModelLibrary.Item
                {
                    Menu = (ModelLibrary.Menu)comboBoxCategory.SelectedValue,
                    Description = textBoxDescription.Text,
                    Name = textBoxName.Text,
                    ItemCat = (ModelLibrary.ItemCat)comboBoxCategory.SelectedValue,
                    Price = newPrice

                };
                proxy.CreateItem(newItem);
               
                var item = proxy.GetItemByNameAndMenuId(newItem.Name, Convert.ToInt32(labelMenuId.Content));
                var itemId = item.Id;
                proxyPrice.CreatePrice(newPrice, itemId);
                var updatedItem = new ModelLibrary.Item
                {
                    Id = item.Id,
                    Description = item.Description,
                    ItemCat = item.ItemCat,
                    Menu = item.Menu,
                    Name = item.Name,
                    Price = item.Price

                };
                proxy.UpdateItem((ModelLibrary.Item)dataGridItemList.SelectedItem, updatedItem);
                dataGridItemList.Items.Clear();
                var modelMenu = proxy.GetAllItemsByRestaurant(1000000);
                foreach (Item itemo in modelMenu)
                {
                    dataGridItemList.Items.Add(itemo);
                };
            }
            else
            {
                MessageBox.Show("Price needs to be a double(NN.N(N))");
            }
        }

        private void buttonCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            ItemCatCrud var = new ItemCatCrud();
            this.NavigationService.Navigate(var);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            var proxy = new ItemServiceClient();
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            itemCtrl.ConvertItemToDb(selectedItem);
            proxy.DeleteItem(selectedItem);
            var modelMenu = proxy.GetAllItemsByRestaurant(1000000);
            dataGridItemList.Items.Clear();
            foreach (Item item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var proxy = new ItemServiceClient();
            var proxyPrice = new PriceServiceClient();
            var proxyMenu = new MenuServiceClient(); 
            double price;
            bool success = double.TryParse(textBoxNamePrice.Text, out price);
            if (success) 
            {
                var newPrice = new ModelLibrary.Price
                {
                    VarPrice = price
                    
                };
                var newItem = new ModelLibrary.Item
                {
                    Description = textBoxDescription.Text.ToString(),
                    Name = textBoxName.Text.ToString(),
                    ItemCat = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem,
                    Menu = proxyMenu.GetMenuById(Convert.ToInt32(1000000)), //labelMenuId.Content
                    Price = newPrice 
                };
                proxy.CreateItem(newItem);
                var menu = proxyMenu.GetMenuById(Convert.ToInt32(labelMenuId.Content));
                var menuId = menu.Id;
                var item = proxy.GetItemByNameAndMenuId(newItem.Name, menuId);
                var itemId = item.Id;
                proxyPrice.CreatePrice(newPrice, itemId);
                dataGridItemList.Items.Clear();
                var modelMenu = proxy.GetAllItemsByRestaurant(1000000);
                foreach (Item itemo in modelMenu)
                {
                    dataGridItemList.Items.Add(itemo);
                };
            }
            else
            {
                MessageBox.Show("Price needs to be a double(NN.N(N))");
            }
        }

        private void dataGridItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proxyCat = new ItemServiceClient();
            var selectedItem = dataGridItemList.SelectedItem as ModelLibrary.Item;
            if (selectedItem == null)
            {
                textBoxName.Text = "";
            }
            else
            {
                textBoxName.Text = dataGridItemList.SelectedItem.ToString();
                textBoxDescription.Text = selectedItem.Description;
                textBoxNamePrice.Text = selectedItem.Price.VarPrice.ToString();
                comboBoxCategory.ItemsSource = proxyCat.GetAllItemCategories();
            }
        }
    }
}
