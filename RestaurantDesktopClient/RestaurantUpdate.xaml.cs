using RestaurantDesktopClient.RestaurantService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for RestaurantUpdate.xaml
    /// </summary>
    public partial class RestaurantUpdate : Page
    {
        public RestaurantUpdate(int restaurantId)
        {
            InitializeComponent();
            resId = restaurantId;
            var proxyRestaurant = new RestaurantServiceClient();
            ModelLibrary.Restaurant res = proxyRestaurant.GetRestaurant(resId);
            comboBoxCategory.ItemsSource = proxyRestaurant.GetAllRestaurantCategories();
            comboBoxCategory.SelectedIndex = 0;
            if(res != null)
            {
                textBoxName.Text = res.Name;
                textBoxAddress.Text = res.Address;
                textBoxZipCode.Text = res.ZipCode;
                textBoxPhone.Text = res.PhoneNo;
                textBoxEmail.Text = res.Email;
                textBoxcategory.Text = res.Category.ToString();
                checkBoxDiscontinued.IsChecked = res.Discontinued;
                checkBoxVerified.IsChecked = res.Verified;
            }
            else
            {
                textBoxName.Text = "";
                textBoxAddress.Text = "";
                textBoxZipCode.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
                textBoxcategory.Text = "";
                checkBoxDiscontinued.IsChecked = false;
                checkBoxVerified.IsChecked = false;

            }
            
        }
        public int resId;

        private bool ValidateRestaurant(ModelLibrary.Restaurant restaurant)
        {
            var context = new ValidationContext(restaurant, null, null);
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return Validator.TryValidateObject(restaurant, context, result, true);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var proxyRestaurant = new RestaurantServiceClient();
            var res = proxyRestaurant.GetRestaurant(resId);
            var modelRes = new ModelLibrary.Restaurant
            {
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                Email = textBoxEmail.Text,
                ZipCode = textBoxZipCode.Text,
                Category = res.Category,
                PhoneNo = textBoxPhone.Text,
                Verified = res.Verified,
                Discontinued = res.Discontinued,
                Id = resId
                
            };
            var validation = ValidateRestaurant(modelRes);
            if(validation == true)
            {
                proxyRestaurant.UpdateRestaurant(modelRes);
            }
            else { MessageBox.Show("Validation did not pass"); }
            
        }
    }
}
