using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        private void RemoveTable_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new RestaurantServiceClient();
            var table = CreateTable();

            if (ValidateTable(table))
            {
                proxy.DeleteTableAsync(table);
            }
            else
            {
                MessageBoxResult prompt = MessageBox.Show("Please enter valid characters in all fields", "Invalid Input");
            }
            ToTableDataGrid();
        }

        private void DataGridTableList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemIndex = dataGridTableList.SelectedIndex;
            var dict = GetTablesInDictionary();
            if (selectedItemIndex > -1)
            {
                HiddenNoSeats.Content = dict.ElementAt(selectedItemIndex).Key;
                TextBoxSeatsPerTable.Text = dict.ElementAt(selectedItemIndex).Key.ToString();
                TextBoxNumberOfTables.Text = dict.ElementAt(selectedItemIndex).Value.ToString();
                TextBoxTotalSeats.Text = (dict.ElementAt(selectedItemIndex).Key * dict.ElementAt(selectedItemIndex).Value).ToString();
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
            var noSeatsNoDuplicates = GetTablesInDictionary();



            var dt = new DataTable();
            dt.Columns.Add("Number of Seats", typeof(int));
            dt.Columns.Add("Number of Tables", typeof(int));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.DefaultView.Delete(i);
            }

            for (var i = 0; i < noSeatsNoDuplicates.Count; i++)
            {
                var dr = dt.NewRow();
                dr[0] = noSeatsNoDuplicates.ElementAt(i).Key;
                dr[1] = noSeatsNoDuplicates.ElementAt(i).Value;

                dt.Rows.Add(dr);
            }

            dataGridTableList.CanUserResizeRows = false;
            dataGridTableList.CanUserResizeColumns = false;
            dataGridTableList.CanUserReorderColumns = false;
            dataGridTableList.ItemsSource = null;
            dataGridTableList.ItemsSource = dt.DefaultView;
            

            return dataGridTableList;
        }

        private Dictionary<int,int> GetTablesInDictionary()
        {

            var proxy = new RestaurantServiceClient();
            var call = proxy.GetAllTablesByRestaurantAsync(Convert.ToInt32(HiddenResId.Content.ToString()));
            var tables = call.Result.ToList();
            var dictionary = new Dictionary<int, int>();

            foreach (var table in tables)
            {
                if (dictionary.ContainsKey(table.NoSeats))
                    dictionary[table.NoSeats]++;
                else
                    dictionary.Add(table.NoSeats, 1);
            }

            return dictionary;
        }
    }
}
