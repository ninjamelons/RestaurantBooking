﻿using System;
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

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for RestaurantHome.xaml
    /// </summary>
    public partial class RestaurantHome : Page
    {
        public RestaurantHome()
        {
            InitializeComponent();
        }

        private void ToTablesPage_OnClick(object sender, RoutedEventArgs e)
        {
            // View TablesCrud Page
            TablesCrud tablesPage = new TablesCrud();
            this.NavigationService.Navigate(tablesPage);
        }
    }
}
