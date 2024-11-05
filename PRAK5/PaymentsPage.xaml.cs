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
    public partial class PaymentsPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();
        public PaymentsPage()
        {
            InitializeComponent();
            RefreshDataGrid();

            OrdersComboBox.ItemsSource = phloraEntities.Orders.ToList();
            OrdersComboBox.DisplayMemberPath = "OrderID";
            OrdersComboBox.SelectedValuePath = "OrderID";
        }
        private void RefreshDataGrid()
        {
            DataGrid8.ItemsSource = phloraEntities.Payments.ToList();
        }

        private void clearInputs()
        {
            AmounT.Text = "";
            DeleteBtnClick.IsEnabled = false;
        }

        private void DataGrid8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
