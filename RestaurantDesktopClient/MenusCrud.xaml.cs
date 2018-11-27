
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
using DatabaseAccessLibrary;
using RestaurantService;

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for MenusCrud.xaml
    /// </summary>
    public partial class MenusCrud : Page
    {
        public MenusCrud()
        {
            InitializeComponent();
            hiddenMenuId.Content = "0";
            MenuService menuService = new MenuService();
            //listBoxItems.Items.Add = me
            //menuNameBox.ItemsSource = GetMenus();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        /*private List<DatabaseAccessLibrary.Item> getItems(int menuId)
        {
            ItemDb itemDb = new ItemDb();
            itemDb.GetItems
            return;
        }*/

        /*public IEnumerable<Menu> GetMenus()
        {
            MenuService menuService = new MenuService();
            return menuService.GetAllMenusByRestaurant(Convert.ToInt32(hiddenMenuId));
            
        }*/
    }
}
