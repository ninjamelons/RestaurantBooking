using RestaurantDesktopClient.ItemService;
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
using System.ComponentModel.DataAnnotations;

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for ItemCatCrud.xaml
    /// </summary>
    public partial class ItemCatCrud : Page
    {
        public ItemCatCrud()
        {
            InitializeComponent();
            
            var modelMenu = Services._ItemProxy.GetAllItemCategories();
            foreach (ItemCat item in modelMenu)
            {
                dataGridItemCatList.Items.Add(item);
            };
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItemCat = (ModelLibrary.ItemCat)dataGridItemCatList.SelectedItem;
            if (selectedItemCat == null)
            {
                MessageBox.Show("Nothing selected");
            }
            else
            {
                await Services._ItemProxy.DeleteItemCatAsync(selectedItemCat.Id);
                dataGridItemCatList.Items.Clear();
                var modelMenu = await Services._ItemProxy.GetAllItemCategoriesAsync();
                foreach (ItemCat item in modelMenu)
                {
                    dataGridItemCatList.Items.Add(item);
                };
            }
        }

        private bool ValidateCat(ModelLibrary.ItemCat itemCat)
        {
            var context = new ValidationContext(itemCat, null, null);
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return Validator.TryValidateObject(itemCat, context, result, true);
        }
        private async void buttonCreate_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxName.Text.Length < 2)
            {
                MessageBox.Show("EmptyName");
            }
            else
            {
                
                var newItemCat = new ItemCat
                {
                    Name = textBoxName.Text
                };
                var validation = ValidateCat(newItemCat);
                if(validation == true)
                {
                    await Services._ItemProxy.CreateItemCatAsync(newItemCat);
                    dataGridItemCatList.Items.Clear();
                    var modelMenu = await Services._ItemProxy.GetAllItemCategoriesAsync();
                    foreach (ItemCat item in modelMenu)
                    {
                        dataGridItemCatList.Items.Add(item);
                    };
                }
                else { MessageBox.Show("Validation did not pass!"); }
                
            }
        }

        private async void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            
            if (textBoxName.Text.Length < 2 )
            {
                MessageBox.Show("InvalidName");
            }
            else
            {
                var slectedItemCat = dataGridItemCatList.SelectedItem as ModelLibrary.ItemCat;
                var oldItem = new ItemCat
                {
                    Name = slectedItemCat.Name,
                    Id = slectedItemCat.Id

                };
                var newItem = new ItemCat
                {
                    Name = textBoxName.Text,
                    Id = slectedItemCat.Id
                };
                var validation = ValidateCat(newItem);
                if (validation == true)
                {
                    await Services._ItemProxy.UpdateItemCatAsync(oldItem, newItem);
                    dataGridItemCatList.Items.Clear();
                    var modelMenu = await Services._ItemProxy.GetAllItemCategoriesAsync();
                    foreach (ItemCat item in modelMenu)
                    {
                        dataGridItemCatList.Items.Add(item);
                    };
                    var selectedItem = dataGridItemCatList.SelectedItem as ModelLibrary.ItemCat;
                }
                else { MessageBox.Show("Validation did not pass!"); }
                    
            }

        }

        private void dataGridItemCatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dataGridItemCatList.SelectedItem as ModelLibrary.ItemCat;
            if (selectedItem == null)
            {
                textBoxName.Text = "";
            }
            else
            {
                textBoxName.Text = dataGridItemCatList.SelectedItem.ToString();
            }                
        }
    }
}
