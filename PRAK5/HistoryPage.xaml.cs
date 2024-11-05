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
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();

        public HistoryPage()
        {
            InitializeComponent();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            DataGrid9.ItemsSource = phloraEntities.OperationHistory.ToList();
        }

        private void clearInputs()
        {
            TypE.Text = "";
            DatE.Text = "";
            DescriptioN.Text = "";
            DeleteBtnClick.IsEnabled = false;
        }

        private void DataGrid9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid9.SelectedItem != null)
            {
                OperationHistory selected = DataGrid9.SelectedItem as OperationHistory;

                if (selected != null)
                {
                    DeleteBtnClick.IsEnabled = true;
                    TypE.Text = selected.OperationType;
                    DatE.Text = selected.OperationDate;
                    DescriptioN.Text = selected.Description;
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string type = TypE.Text;
            string date = DatE.Text;
            string des = DescriptioN.Text;


            OperationHistory employees = new OperationHistory { OperationType = type, OperationDate = date, Description = des};

            phloraEntities.OperationHistory.Add(employees);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid9.SelectedItem != null)
            {
                 OperationHistory selected = DataGrid9.SelectedItem as OperationHistory;

                if (selected != null)
                {
                    phloraEntities.OperationHistory.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();

                    clearInputs();
                }
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid9.SelectedItem != null)
            {
                OperationHistory selected = DataGrid9.SelectedItem as OperationHistory ;
                string des = DescriptioN.Text;
                string date = DatE.Text;
                string type = TypE.Text;


                selected.OperationType = type;
                selected.OperationDate = date;
                selected.Description = des;
                phloraEntities.SaveChanges();
                RefreshDataGrid();
                clearInputs();
            }
        }
    }
}
