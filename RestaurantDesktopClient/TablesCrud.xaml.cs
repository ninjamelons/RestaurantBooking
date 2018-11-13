using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using ModelLibrary;
using RestaurantDesktopClient.RestaurantService;

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
            //ResId.Content = GetRestaurantId();
            TableCombo.ItemsSource = GetTables();
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

        private void TableCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var selectedTable = (ModelLibrary.Table)TableCombo.SelectedItem;
            var dbTable = proxy.GetTable(selectedTable);
            NoSeats.Text = dbTable.NoSeats;
            NoReserved.Text = dbTable.Reserved;
            NoTotal.Text = dbTable.Total;
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

        private IEnumerable<Table> GetTables()
        {
            var proxy = new RestaurantServiceClient();
            return proxy.GetAllTables(Convert.ToInt32(ResId.Content.ToString()));
        }
    }
}
