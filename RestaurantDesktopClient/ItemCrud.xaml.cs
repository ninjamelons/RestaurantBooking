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
            //comboBoxCategory.ItemsSource = GetItemCats();// ??????????????
            comboBoxMenu.ItemsSource = GetMenus();
            var proxyMenu = new MenuServiceClient();
            var proxy = new ItemServiceClient();
            var modelMenu = proxy.GetAllItemsByRestaurant(1000000);
            foreach (Item item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
        }

        private IEnumerable<ModelLibrary.Menu> GetMenus()
        {
            var proxy = new MenuServiceClient();

            return proxy.GetAllMenusByRestaurant(1000000);
        }


        private IEnumerable<ModelLibrary.ItemCat> GetItemCats()
        {
            var proxy = new ItemServiceClient();

            return proxy.GetAllItemCategories();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var proxyPrice = new PriceServiceClient();
            var proxy = new ItemServiceClient();
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
                    Menu = (ModelLibrary.Menu)comboBoxCategory.SelectedValue,
                    Description = textBoxDescription.Text,
                    Name = textBoxName.Text,
                    ItemCat = (ModelLibrary.ItemCat)label3.Content,//(ModelLibrary.ItemCat)comboBoxCategory.SelectedValue,
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
            var dbItem = proxy.GetItem(selectedItem);
            proxy.DeleteItem(dbItem);
            dataGridItemList.Items.Clear();
            var modelMenu = proxy.GetAllItemsByRestaurant(1000000);
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
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            var dbItem = proxy.GetItem(selectedItem);
            double price;
            bool success = double.TryParse(textBoxNamePrice.Text, out price);
            if (success)
            {
                var newPrice = new ModelLibrary.Price
                {
                    VarPrice = price,

                };
                var newItem = new ModelLibrary.Item
                {
                    Description = textBoxDescription.Text.ToString(),
                    Name = textBoxName.Text.ToString(),
                    ItemCat = dbItem.ItemCat,//(ModelLibrary.ItemCat)comboBoxCategory.SelectedItem,
                    Menu = dbItem.Menu,//(ModelLibrary.Menu)comboBoxMenu.SelectedItem, //labelMenuId.Content
                    Price = newPrice
                };
                proxy.CreateItem(newItem);
                var item = proxy.GetItem(newItem);
                proxyPrice.CreatePrice(newPrice, dbItem.Id);
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
            var proxy = new ItemServiceClient();
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            var dbItem = proxy.GetItem(selectedItem);
            textBoxDescription.Text = dbItem.Description;
            textBoxName.Text = dbItem.Name;
            textBoxNamePrice.Text = dbItem.Price.VarPrice.ToString();
            textBoxMenuId.Text = dbItem.Menu.Id.ToString();
            textBoxCategoryId.Text = dbItem.ItemCat.Id.ToString();
            label1.Content = dbItem.Id;
            label2.Content = dbItem.Menu.Id;
            label3.Content = dbItem.ItemCat;

        }

    }
}
