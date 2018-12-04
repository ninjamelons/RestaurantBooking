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
            hiddenResId.Content = "1000000";
            //hiddenResId.Content = GetRestaurantId();
            TableCombo.ItemsSource = GetTables();
        }

        private void UpdateAddTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var oldTable = new Table
            {
         //       NoSeats = hiddenNoSeats.Content.ToString(),
          //      RestaurantId = hiddenResId.Content.ToString()
            };
            var newTable = CreateTable();

            if (ValidateTable(newTable))
            {
                if (CheckOldTableMatchesDb(oldTable))
                {
                    proxy.UpdateTable(oldTable,newTable);
                }
            //    else if (newTable.RestaurantId != hiddenResId.Content.ToString()
           //              && newTable.NoSeats != hiddenNoSeats.Content.ToString())
           //     {
             //       proxy.CreateTable(newTable);
           //     }
                else
                {
                    MessageBoxResult prompt =
                        MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");
                }
            }
        }

        private bool CheckOldTableMatchesDb(Table oldTable)
        {
            var proxy = new RestaurantServiceClient();
            if (proxy.GetTable(oldTable) != null)
                return true;
            return false;
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
            hiddenNoSeats.Content = dbTable.NoSeats;
       //     NoSeats.Text = dbTable.NoSeats;
       //     NoReserved.Text = dbTable.Reserved;
       //     NoTotal.Text = dbTable.Total;
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
             //   NoSeats = NoSeats.Text, Reserved = NoReserved.Text,
             //   RestaurantId = hiddenResId.Content.ToString(), Total = NoTotal.Text
            };
        }

        private IEnumerable<Table> GetTables()
        {
            var proxy = new RestaurantServiceClient();
            return proxy.GetAllTables(Convert.ToInt32(hiddenResId.Content.ToString()));
        }
    }
}
