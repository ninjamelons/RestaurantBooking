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
using System.Transactions;
using System.ComponentModel.DataAnnotations;

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
            InitializeComponent();
            var menu = Services._MenuProxy.GetMenuById(amenuId);
            labelMenuName.Content = menu.Id;
            comboBoxCategory.ItemsSource = GetItemCats();
            //comboBoxCategory.SelectedItem = 
            dataGridItemList.SelectedItem = 1;
            var modelMenu = Services._ItemProxy.GetAllItemsByMenu(menuId);
            foreach (Item item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
        }
        public int amenuId;
        public int arestaurantId;
        

        private async Task<IEnumerable<ModelLibrary.Menu>> GetMenus()
        {
            return await Services._MenuProxy.GetAllMenusByRestaurantAsync(arestaurantId);
        }


        private IEnumerable<ModelLibrary.ItemCat> GetItemCats()
        {
            return Services._ItemProxy.GetAllItemCategories();
        }
        private bool ValidateItem(ModelLibrary.Item item)
        {
            var context = new ValidationContext(item, null, null);
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return Validator.TryValidateObject(item, context, result, true);
        }

        private bool ValidatePrice(ModelLibrary.Price price)
        {
            var context = new ValidationContext(price, null, null);
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return Validator.TryValidateObject(price, context, result, true);
        }
        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            double price;
            ModelLibrary.Item selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            
                
                bool success = double.TryParse(textBoxNamePrice.Text, out price);
                if (success)

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
                    await Services._PriceProxy.UpdatePriceAsync(priceo, selectedItem.Id);

                    var catItem = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem;
                    var selectedCatId = catItem == null ? 0 : catItem.Id;

                    if (catItem != null)
                    {
                    var itemValidation = ValidateItem(updatedItem);
                    var priceValidation = ValidatePrice(priceo);
                    if( itemValidation == true && priceValidation == true)
                    {
                        await Services._ItemProxy.UpdateItemAsync(updatedItem, amenuId, selectedCatId); // IFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
                        dataGridItemList.Items.Clear();
                        var modelMenu = await Services._ItemProxy.GetAllItemsByMenuAsync(amenuId);
                        foreach (Item itemo in modelMenu)
                        {
                            dataGridItemList.Items.Add(itemo);
                        };
                    }
                    else { MessageBox.Show("Validation did not pass!"); }
                        
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

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            
            ItemCtrl itemCtrl = new ItemCtrl();
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            var dbItem = await Services._ItemProxy.GetItemAsync(selectedItem.Id);
            if(dbItem == null)
            {
                MessageBox.Show("Select an item for deletion");
            }
            await Services._ItemProxy.DeleteItemAsync(selectedItem.Id);
            dataGridItemList.Items.Clear();
            var modelMenu = await Services._ItemProxy.GetAllItemsByMenuAsync(amenuId);
            foreach (Item item in modelMenu)
            {
                dataGridItemList.Items.Add(item);
            };
        }

        private async void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            double price;
            bool success = double.TryParse(textBoxNamePrice.Text, out price);
            if (success)
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
                    var validation = ValidateItem(item);
                    var priceValidation = ValidatePrice(priceObj);
                    if(validation == true && priceValidation)
                    {
                        var selectedCategory = (ModelLibrary.ItemCat)comboBoxCategory.SelectedItem;
                        await Services._ItemProxy.CreateItemAsync(item, amenuId, selectedCategory.Id);
                        var returnedItem = await Services._ItemProxy.GetItemByNameAsync(item.Name);
                        await Services._PriceProxy.CreatePriceAsync(priceObj, returnedItem.Id);
                        dataGridItemList.Items.Clear();
                        var modelMenu = await Services._ItemProxy.GetAllItemsByMenuAsync(amenuId);
                        foreach (Item itemo in modelMenu)
                        {
                            dataGridItemList.Items.Add(itemo);
                        };
                        
                    }
                    else { MessageBox.Show("Validation did not pass"); }
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

        private async void dataGridItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ModelLibrary.Item)dataGridItemList.SelectedItem;
            if (selectedItem != null)
            {
                textBoxDescription.Text = selectedItem.Description;
                textBoxName.Text = selectedItem.Name;
                var price = await Services._PriceProxy.GetLatestPriceAsync(selectedItem.Id);//proxyPrice.GetPrice(returnedItem.Id);
                textBoxNamePrice.Text = price.VarPrice.ToString();//price.VarPrice.ToString();
                label1.Content = selectedItem.Id;
                label2.Content = selectedItem.Description;
                label3.Content = selectedItem.Name;
                label4.Content = selectedItem.Id;
                var mmenu = await Services._MenuProxy.GetMenuByIdAsync(amenuId);
                var categoryy = await Services._ItemProxy.GetCatByItemCatIdAsync(selectedItem.Id);
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

