using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
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
        public TablesCrud(int resId)
        {
            InitializeComponent();
            HiddenResId.Content = resId;
            ToTableDataGrid();
        }

        private void UpdateTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var oldTable = new Table
            {
                 NoSeats = Convert.ToInt32(HiddenNoSeats.Content.ToString()),
                RestaurantId = Convert.ToInt32(HiddenResId.Content.ToString())
            };
            var newTable = CreateTable();

            if (ValidateTable(newTable))
            {
                if (CheckOldTableMatchesDb(oldTable))
                {
                    proxy.UpdateTable(oldTable, newTable);
                }
                else
                {
                    MessageBoxResult prompt =
                        MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");
                }
            }
        }

        private void AddTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var table = new Table
            {
                NoSeats = Convert.ToInt32(TextBoxSeatsPerTable.Text),
                RestaurantId = Convert.ToInt32(HiddenResId.Content.ToString())
            };
            if (ValidateTable(table))
                proxy.CreateTable(table);
            else
            {
                MessageBoxResult prompt =
                    MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");
            }
        }

        private bool CheckOldTableMatchesDb(Table oldTable)
        {
            var proxy = new RestaurantServiceClient();
            if (proxy.GetTable(oldTable) != null)
                return true;
            return false;
        }

        private void RemoveTable_OnClick(object sender, RoutedEventArgs e)
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

        private void DataGridTableList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            //var selectedTable = (ModelLibrary.Table)dataGridTableList.SelectedItem;
            //var dbTable = proxy.GetTable(selectedTable);
           // HiddenNoSeats.Content = dbTable.NoSeats;
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
            return proxy.GetAllTablesByRestaurant(Convert.ToInt32(HiddenResId.Content.ToString()));
        }

        private DataGrid ToTableDataGrid()
        {
            var proxy = new RestaurantServiceClient();
            var tables = proxy.GetAllTablesByRestaurant(Convert.ToInt32(HiddenResId.Content.ToString())).ToList();
            var noSeats = new int[tables.Count];
            var noSeatsNoDuplicates = new int[5, 2];
            var j = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("Number of Seats", typeof(int));
            dt.Columns.Add("Number of Tables", typeof(int));

            for (var i = 0; i < tables.Count; i++)
            {
                noSeats[i] = tables[i].NoSeats;
                if (i == 0 || noSeats[i] != noSeats[i-1])
                {
                    noSeatsNoDuplicates[j,0] = noSeats[i];
                    j++;
                }
            }

            foreach (var noSeat in noSeats)
            {
                for (var i = 0; i < noSeatsNoDuplicates.GetLength(0); i++)
                {
                    if (noSeat == noSeatsNoDuplicates[i,0])
                    {
                        noSeatsNoDuplicates[i,1]++;
                    }
                }
            }

            for (var i = 0; i < noSeatsNoDuplicates.GetLength(0); i++)
            {
                var dr = dt.NewRow();
                for (var k = 0; k < 2; k++)
                {
                    dr[k] = noSeatsNoDuplicates[i,k];
                }

                dt.Rows.Add(dr);
            }

            dataGridTableList.CanUserResizeRows = false;
            dataGridTableList.CanUserResizeColumns = false;

            dataGridTableList.ItemsSource = dt.DefaultView;

            return dataGridTableList;
        }
    }
}
