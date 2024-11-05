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
using System.Windows.Shapes;

namespace PRAK5
{
    public partial class Manager : Window
    {
        private int userAccessLevel = AccessLevel.UserLevel;

        public Manager()
        {
            InitializeComponent();
            Page.Content = new CustomersPage();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
            userAccessLevel = Session.AccessLevel;
        }

        private void EmployeeClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new EmployeePage();
        }

        private void CustomersClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new CustomersPage();
        }

        private void СloseManagerClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userAccessLevel == AccessLevel.AdminLevel)
            {
                Page.Content = new AccountsPage();
            } else
            {
                MessageBox.Show("К сожалению, управляет данными аккаунтов может только " + AccessLevel.AdminRole);
            }
        }

        private void ReviewsClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new ReviewsPage();
        }

        private void ProductsClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new ProductsPage();
        }

        private void OrdersClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new OrdersPage();
        }

        private void DetailClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new OrderDetails();

        }
        private void PaymentsClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new PaymentsPage();

        }

        private void HistoryClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new HDetails();

        }
        private void SuppliersClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new OrderDetails();

        }
        private void InventoryClick(object sender, RoutedEventArgs e)
        {
            Page.Content = new OrderDetails();

        }


    }
}
