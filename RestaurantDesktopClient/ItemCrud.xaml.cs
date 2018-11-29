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
        public ItemCrud()
        {
            InitializeComponent();
            var proxyMenu = new MenuServiceClient();
            var proxy = new ItemServiceClient();
            comboBoxMenuId.ItemsSource = proxyMenu.GetAllMenusByRestaurant(1000000);
            var modelMenu = proxy.GetAllItemsByRestaurant(1000000);
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
                    Menu = (ModelLibrary.Menu)comboBoxMenuId.SelectedItem,
                    Description = textBoxDescription.Text,
                    Name = textBoxName.Text,
                    ItemCat = (ModelLibrary.ItemCat)comboBoxCategory.SelectedValue,
                    Price = newPrice

                };
                proxy.CreateItem(newItem);
                var menu = (ModelLibrary.Menu)comboBoxMenuId.SelectedItem as ModelLibrary.Menu;
                var menuId = menu.Id;
                var item = proxy.GetItemByNameAndMenuId(newItem.Name, menuId);
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
                    Description = textBoxDescription.Text,
                    Name = textBoxName.Text,
                    ItemCat = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem,
                    Menu = (ModelLibrary.Menu)comboBoxMenuId.SelectedItem,
                    Price = newPrice
                };
                proxy.CreateItem(newItem);
                var menu = (ModelLibrary.Menu)comboBoxMenuId.SelectedItem as ModelLibrary.Menu;
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
    }
}
