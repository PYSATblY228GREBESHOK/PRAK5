using System;
using System.Collections.Generic;
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

namespace PRAK5
{
    public partial class OrderDetails : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();
        public OrderDetails()
        {
            InitializeComponent();
            RefreshDataGrid();

            OrdersComboBox.ItemsSource = phloraEntities.Orders.ToList();
            OrdersComboBox.DisplayMemberPath = "OrderID";
            OrdersComboBox.SelectedValuePath = "OrderID";

            ProductsComboBox.ItemsSource = phloraEntities.Products.ToList();
            ProductsComboBox.DisplayMemberPath = "Name";
            ProductsComboBox.SelectedValuePath = "ProductID";
        }

        private void RefreshDataGrid()
        {
            DataGrid6.ItemsSource = phloraEntities.OrderDetails.ToList();
        }
        private void clearInputs()
        {
            QuantitY.Text = "";
            PricePerUniT.Text = "";
            DeleteBtnClick.IsEnabled = false;
        }
        private void TextDetails(object sender, TextChangedEventArgs e)
        {
            string Quantity = QuantitY.Text;
            string PricePerUnit = PricePerUniT.Text;

            if (string.IsNullOrEmpty(Quantity) || string.IsNullOrEmpty(PricePerUnit))
            {
                AddBtnClick.IsEnabled = false;
                EditBtnClick.IsEnabled = false;
            } else {
                AddBtnClick.IsEnabled = true;
                EditBtnClick.IsEnabled = true;
            }

        }

        private void DataGrid6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid6.SelectedItem != null)
            {
                OrderDetails selected = DataGrid6.SelectedItem as OrderDetails;

                if (selected != null)
                {
                    DeleteBtnClick.IsEnabled = true;
                    QuantitY.Text = selected.Quantity;
                    PricePerUniT.Text = selected.PricePerUnit;
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string quantity = QuantitY.Text;
            string PricePerUnit = PricePerUniT.Text;
            int orderID = (int)OrdersComboBox.SelectedValue;
            int productID = (int)ProductsComboBox.SelectedValue;


            OrderDetails details = new OrderDetails { QuantitY = QuantitY, PricePerUniT = PricePerUniT, OrderID = orderID, ProductID = productID };

            phloraEntities.OrderDetails.Add(details);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid6.SelectedItem != null)
            {
                OrderDetails selected = DataGrid6.SelectedItem as OrderDetails;

                if (selected != null)
                {
                    phloraEntities.OrderDetails.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();

                    clearInputs();
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid6.SelectedItem != null)
            {
                OrderDetails selected = DataGrid6.SelectedItem as OrderDetails;
                string quantity = QuantitY.Text;
                string PricePerUnit = PricePerUniT.Text;


                selected.PricePerUniT = PricePerUniT;
                selected.QuantitY = QuantitY;
                phloraEntities.SaveChanges();
                RefreshDataGrid();
                clearInputs();
            }
        }
    }
}
