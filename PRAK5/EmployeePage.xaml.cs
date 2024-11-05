using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
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

namespace PRAK5
{
    public partial class EmployeePage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();
        public EmployeePage()
        {
            InitializeComponent();
            RefreshDataGrid();


            AccountListComboBox.ItemsSource = phloraEntities.Accounts.ToList();
            AccountListComboBox.DisplayMemberPath = "Login";
            AccountListComboBox.SelectedValuePath = "AccountID";
        }

        private void RefreshDataGrid()
        {
            DataGrid2.ItemsSource = phloraEntities.Employees.ToList();
        }

        private void clearInputs()
        {
            FirstName.Text = "";
            LastName.Text = "";
            Position.Text = "";
            DeleteButtonClick.IsEnabled = false;
        }

        private void TextEmployee(object sender, TextChangedEventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string position = Position.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(position))
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
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string position = Position.Text;
            int accountID = (int) AccountListComboBox.SelectedValue;


            Employees employees = new Employees { FirstName = firstName, LastName = lastName, Position = position, AccountID = accountID};

            phloraEntities.Employees.Add(employees);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                Employees selected = DataGrid2.SelectedItem as Employees;

                if (selected != null)
                {
                    phloraEntities.Employees.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();

                    clearInputs();
                }
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                Employees selected = DataGrid2.SelectedItem as Employees;
                string firstName = FirstName.Text;
                string lastName = LastName.Text;
                string position = Position.Text;


                selected.FirstName = firstName;
                selected.LastName = lastName;
                selected.Position = position;
                phloraEntities.SaveChanges();
                RefreshDataGrid();
                clearInputs();
            }
        }

        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {
                Employees selected = DataGrid2.SelectedItem as Employees;

                if (selected != null)
                {
                    DeleteButtonClick.IsEnabled = true;
                    FirstName.Text = selected.FirstName;
                    LastName.Text = selected.LastName;
                    Position.Text = selected.Position;
                }
            }
        }
    }
}
