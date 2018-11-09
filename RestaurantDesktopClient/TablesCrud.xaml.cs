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
using RestaurantDesktopClient.RestaurantService;
using IRestaurantService = RestaurantService.IRestaurantService;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for TablesCrud.xaml
    /// </summary>
    public partial class TablesCrud : Page
    {
        public TablesCrud()
        {
            InitializeComponent();
        }

        private void UpdateAddTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var table = CreateTable();

            if (ValidateTable(table))
            {
                proxy.CreateTable(table);
            }
            else
            {
                MessageBoxResult prompt = MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");  
            }
        }

        private void RemoveTable_OnClickTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var table = CreateTable();

            if (ValidateTable(table))
            {
                proxy.DeleteTable(table);
            }
            else
            {
                MessageBoxResult prompt = MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");  
            }
        }

        private bool ValidateTable(ModelLibrary.Table table)
        {
            var context = new ValidationContext(table, null, null);
            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            return Validator.TryValidateObject(table, context, result, true);
        }

        private ModelLibrary.Table CreateTable()
        {
            return new ModelLibrary.Table
            {
                NoSeats = NoSeats.Text, Reserved = NoReserved.Text,
                RestaurantId = ResId.Content.ToString(), Total = NoTotal.Text
            };
        }
    }
}
