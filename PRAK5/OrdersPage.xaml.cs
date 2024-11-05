using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PRAK5
{

    public partial class OrdersPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();
        public OrdersPage()
        {
            InitializeComponent();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            DataGrid5.ItemsSource = phloraEntities.Employees.ToList();
        }
        private void clearInputs()
        {
            OrderDate.Text = "";
            TotalAmounT.Text = "";
            DeleteButtonClick.IsEnabled = false;
        }
        private void TextOrders(object sender, TextChangedEventArgs e)
        {
            string TotalAmount = TotalAmounT.Text;
            string OrdersDate = OrderDate.Text;

            if (string.IsNullOrEmpty(TotalAmount) || string.IsNullOrEmpty(OrdersDate))
            {
                AddButtonClick.IsEnabled = false;
                EditButtonClick.IsEnabled = false;
            }
            else
            {
                AddButtonClick.IsEnabled = true;
                EditButtonClick.IsEnabled = true;
            }

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            string TotalAmount = TotalAmounT.Text;
            string OrdersDate = OrderDate.Text;


            Orders orders = new Orders { TotalAmount = TotalAmount, OrderDate = OrdersDate};

            phloraEntities.Orders.Add(orders);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid5.SelectedItem != null)
            {
                Orders selected = DataGrid5.SelectedItem as Orders;

                if (selected != null)
                {
                    phloraEntities.Orders.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();

                    clearInputs();
                }
            }
        } 

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid5.SelectedItem != null)
            {
                Orders selected = DataGrid5.SelectedItem as Orders;
                string TotalAmount = TotalAmounT.Text;
                string OrdersDate = OrderDate.Text;


                selected.OrderDate = OrdersDate;
                selected.TotalAmount = TotalAmount;
                phloraEntities.SaveChanges();
                RefreshDataGrid();
                clearInputs();
            }
        }

        private void DataGrid5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid5.SelectedItem != null)
            {
                Orders selected = DataGrid5.SelectedItem as Orders;

                if (selected != null)
                {
                    DeleteButtonClick.IsEnabled = true;
                    TotalAmounT.Text = selected.TotalAmount;
                    OrderDate.Text = selected.OrderDate;
                }
            }
        }
    }
}
    

