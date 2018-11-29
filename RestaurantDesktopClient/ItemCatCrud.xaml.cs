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
            var proxy = new ItemServiceClient();
            var modelMenu = proxy.GetAllItemCategories();
            foreach (ItemCat item in modelMenu)
            {
                dataGridItemCatList.Items.Add(item);
            };
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItemCat = (ModelLibrary.ItemCat)dataGridItemCatList.SelectedItem; 
            //labelRestaurantId = Convert.ToInt32()  
            var proxy = new ItemServiceClient();
            proxy.DeleteItemCat(selectedItemCat);
            dataGridItemCatList.Items.Clear();
            var modelMenu = proxy.GetAllItemCategories();
            foreach (ItemCat item in modelMenu)
            {
                dataGridItemCatList.Items.Add(item);
            };


        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            var newItemCat = new ItemCat
            {
                Name = textBoxName.Text
            };

            var proxy = new ItemServiceClient();
            proxy.CreateItemCat(newItemCat);
            dataGridItemCatList.Items.Clear();
            var modelMenu = proxy.GetAllItemCategories();
            foreach (ItemCat item in modelMenu)
            {
                dataGridItemCatList.Items.Add(item);
            };
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            
            
        }
    }
}
