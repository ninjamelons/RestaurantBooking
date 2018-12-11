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
            HiddenNoSeats.Content = TextBoxSeatsPerTable.Text;
            var newTable = CreateTable();

            if (ValidateTable(newTable))
            {
                if (CheckOldTableMatchesDb(oldTable))
                {
                    var selectedItemIndex = dataGridTableList.SelectedIndex;
                    for (var i = 0; i < GetTablesInIntArray()[selectedItemIndex, 1]; i++)
                    {
                        proxy.UpdateTableAsync(oldTable, newTable);
                    }
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
            {
                proxy.CreateTableAsync(table);
                ToTableDataGrid();
            }
            else
            {
                MessageBoxResult prompt =
                    MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");
            }
        }

        private bool CheckOldTableMatchesDb(Table oldTable)
        {
            var proxy = new RestaurantServiceClient();
            if (proxy.GetTableAsync(oldTable) != null)
                return true;
            return false;
        }

        private void RemoveTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var table = CreateTable();

            if (ValidateTable(table))
            {
                proxy.DeleteTableAsync(table);
                ToTableDataGrid();
            }
            else
            {
                MessageBoxResult prompt = MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");
            }
        }

        private void DataGridTableList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemIndex = dataGridTableList.SelectedIndex;
            var intArray = GetTablesInIntArray();
            HiddenNoSeats.Content = intArray[selectedItemIndex, 0];
            HiddenNoSeats.Content = intArray[selectedItemIndex, 0];
            TextBoxSeatsPerTable.Text = intArray[selectedItemIndex, 0].ToString();
            TextBoxNumberOfTables.Text = intArray[selectedItemIndex, 1].ToString();
            TextBoxTotalSeats.Text = (intArray[selectedItemIndex,0] * intArray[selectedItemIndex,1]).ToString();
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
                NoSeats = int.Parse(HiddenNoSeats.Content.ToString()),
                RestaurantId = int.Parse(HiddenResId.Content.ToString())
            };
        }

        private IEnumerable<Table> GetTables()
        {
            var proxy = new RestaurantServiceClient();
            return proxy.GetAllTablesByRestaurant(Convert.ToInt32(HiddenResId.Content.ToString()));
        }

        private DataGrid ToTableDataGrid()
        {
            var noSeatsNoDuplicates = GetTablesInIntArray();

            var dt = new DataTable();
            dt.Columns.Add("Number of Seats", typeof(int));
            dt.Columns.Add("Number of Tables", typeof(int));

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
            dataGridTableList.CanUserReorderColumns = false;

            dataGridTableList.ItemsSource = dt.DefaultView;

            return dataGridTableList;
        }

        private int[,] GetTablesInIntArray()
        {

            var proxy = new RestaurantServiceClient();
            var call = proxy.GetAllTablesByRestaurantAsync(Convert.ToInt32(HiddenResId.Content.ToString()));
            var tables = call.Result.ToList();
            var noSeats = new int[tables.Count];
            var noSeatsNoDuplicates = new int[5, 2];
            var j = 0;

            for (var i = 0; i < tables.Count; i++)
            {
                noSeats[i] = tables[i].NoSeats;
                if (i == 0 || noSeats[i] != noSeats[i - 1])
                {
                    noSeatsNoDuplicates[j, 0] = noSeats[i];
                    j++;
                }
            }

            foreach (var noSeat in noSeats)
            {
                for (var i = 0; i < noSeatsNoDuplicates.GetLength(0); i++)
                {
                    if (noSeat == noSeatsNoDuplicates[i, 0])
                    {
                        noSeatsNoDuplicates[i, 1]++;
                    }
                }
            }

            return noSeatsNoDuplicates;
        }
    }
}
