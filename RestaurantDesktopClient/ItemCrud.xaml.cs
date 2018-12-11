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
        
        public ItemCrud(int menuId, int restaurantId)
        {
            amenuId = menuId;
            arestaurantId = restaurantId;
            var proxyMenu = new MenuServiceClient();
            InitializeComponent();
            var menu = proxyMenu.GetMenuById(amenuId);
            labelMenuName.Content = menu.Id;
            comboBoxCategory.ItemsSource = GetItemCats();
            //comboBoxCategory.SelectedItem = 
            dataGridItemList.SelectedItem = 1;
            var proxy = new ItemServiceClient();
            var modelMenu = proxy.GetAllItemsByMenu(menuId);
            foreach (Item item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
        }
        public int amenuId;
        public int arestaurantId;
        

        private IEnumerable<ModelLibrary.Menu> GetMenus()
        {
            var proxy = new MenuServiceClient();

            return proxy.GetAllMenusByRestaurant(arestaurantId);
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
            var proxyMenu = new MenuServiceClient();
            double price;
            ModelLibrary.Item selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            bool success = double.TryParse(textBoxNamePrice.Text, out price);
            if (success )

            {
                ModelLibrary.Price priceo = new ModelLibrary.Price
                {
                    VarPrice = double.Parse(textBoxNamePrice.Text)
                    
                };

                ModelLibrary.Item updatedItem = new ModelLibrary.Item
                {
                    Id = selectedItem.Id,
                    Description = textBoxDescription.Text,
                    Name = textBoxName.Text,
                    Price = priceo

                };
                proxyPrice.UpdatePrice(priceo, selectedItem.Id);
                //var categoryId = proxy.GetItemCatByName(comboBoxCategory.SelectedItem.ToString());
                //var menuId = proxyMenu.GetMenuByName(comboBoxMenu.SelectedItem.ToString());
                //var menuItem = (ModelLibrary.Menu)comboBoxMenu.SelectedItem;
                var catItem = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem;
                var selectedCatId = catItem.Id;
                
                if ( catItem != null)
                {
                    proxy.UpdateItem(updatedItem, amenuId, selectedCatId); // IFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
                    dataGridItemList.Items.Clear();
                    var modelMenu = proxy.GetAllItemsByMenu(amenuId);
                    foreach (Item itemo in modelMenu)
                    {
                        dataGridItemList.Items.Add(itemo);
                    };
                }
                else
                {
                    MessageBox.Show("Select category");
                }
               
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
            var dbItem = proxy.GetItem(selectedItem.Id);
            if(dbItem == null)
            {
                MessageBox.Show("Select an item for deletion");
            }
            proxy.DeleteItem(selectedItem.Id);
            dataGridItemList.Items.Clear();
            var modelMenu = proxy.GetAllItemsByMenu(amenuId);
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
            double price;
            bool success = double.TryParse(textBoxNamePrice.Text, out price);
            if (success || string.IsNullOrEmpty(textBoxName.Text) != true || textBoxName.Text.Count() > 20 || textBoxName.Text.Count() < 3
                    || textBoxDescription.Text.Count() < 3 || textBoxDescription.Text.Count() > 150)
            {
                var priceObj = new ModelLibrary.Price
                {
                    VarPrice = price,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(100)

                };
                
                    var item = new ModelLibrary.Item
                    {
                        Description = textBoxDescription.Text,
                        Name = textBoxName.Text,
                        Price = priceObj
                    };

                    //var itemCat = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem;
                    if (comboBoxCategory.SelectedItem != null)
                    {
                        var selectedCategory = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem;
                        proxy.CreateItem(item, amenuId, selectedCategory.Id);
                        var returnedItem = proxy.GetItemByName(item.Name);
                        proxyPrice.CreatePrice(priceObj, returnedItem.Id);
                        dataGridItemList.Items.Clear();
                        var modelMenu = proxy.GetAllItemsByMenu(amenuId);
                        foreach (Item itemo in modelMenu)
                        {
                            dataGridItemList.Items.Add(itemo);
                        };
                    }
                    else
                    {
                        MessageBox.Show("Select category");
                    }

            }
            else
            {
                MessageBox.Show("Invalind Name and/or Description,Price");
            }
        }

        private void dataGridItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proxyPrice = new PriceServiceClient();
            var proxy = new ItemServiceClient();
            var proxyMenu = new MenuServiceClient();
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            if (selectedItem != null)
            {
                textBoxDescription.Text = selectedItem.Description;
                textBoxName.Text = selectedItem.Name;
                var price = proxyPrice.GetLatestPrice(selectedItem.Id);//proxyPrice.GetPrice(returnedItem.Id);
                textBoxNamePrice.Text = price.VarPrice.ToString();//price.VarPrice.ToString();
                label1.Content = selectedItem.Id;
                label2.Content = selectedItem.Description;
                label3.Content = selectedItem.Name;
                label4.Content = selectedItem.Id;
                var mmenu = proxyMenu.GetMenuById(amenuId);
                var categoryy = proxy.GetCatByItemCatId(selectedItem.Id);
                comboBoxCategory.SelectedItem = categoryy.Name;
                
            }
            else
            {
                textBoxDescription.Text = " ";
                textBoxName.Text = " ";
                textBoxNamePrice.Text = " ";//price.VarPrice.ToString();
                label1.Content = " ";
                label2.Content = " ";
                label3.Content = " ";
                label4.Content = " ";
            }
            

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ItemCatCrud tablesPage = new ItemCatCrud();
            this.NavigationService.Navigate(tablesPage);

        }
    }

    }

