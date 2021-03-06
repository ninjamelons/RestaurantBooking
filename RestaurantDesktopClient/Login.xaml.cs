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
using RestaurantDesktopClient.CustomerService;
using ModelLibrary;

namespace RestaurantDesktopClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            // View TablesCrud Page
            var cust = await LoginCustomerAsync();
            if (cust != null)
            {
                RestaurantHome homePage = new RestaurantHome(cust.Id);
                this.NavigationService.Navigate(homePage);
            }
            else
            {
                InvalidLabel.Visibility = Visibility.Hidden;
                InvalidLabel.Visibility = Visibility.Visible;
            }
        }

        
        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            
            e.Handled = true;
            MessageBox.Show("Enter pressed");
        }
        
        private async Task<Customer> LoginCustomerAsync()
        {
            string email = mailText.Text;
            string password = passText.Password;
            password = PasswordEncrypt.HashPasswordString(email, password);
            var customer = await Services._CustomerProxy.LoginCustomerAsync(email, password);

            return customer == null ? null :
                customer.RoleId != 2 ? null : customer;
        }
    }
}
