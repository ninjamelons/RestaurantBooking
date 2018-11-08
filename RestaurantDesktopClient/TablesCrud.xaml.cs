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
        }

        private void UpdateAddTable_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveTable_OnClickTable_OnClick(object sender, RoutedEventArgs e)
        {
            RestaurantServiceClient proxy = new RestaurantServiceClient();

            proxy.DeleteTable(new RestaurantService.Table
            {
                NoSeats = NoSeats.Text, Reserved = NoReserved.Text,
                RestaurantId = ResId.Content.ToString(), Total = NoTotal.Text
            });
        }
    }
}
