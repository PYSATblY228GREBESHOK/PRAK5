using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class CustomersPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();

        public CustomersPage()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            //DataGrid1.ItemsSource = context.Roles.ToList();
            DataGrid1.ItemsSource = phloraEntities.Customers.ToList();
        }


        private bool IsValidInput(string input)
        {
            string pattern = @"^[\p{L}0-9_ ]*$";
            //string pattern = @"^lol[\p{L}0-9_ ]*$";
            return Regex.IsMatch(input, pattern);
        }

        private bool IsValidPhone(string input)
        {
            string pattern = @"^[0-9]*$";
            return Regex.IsMatch(input, pattern);
        }

        private void clearInputs()
        {
            TextFirstName.Text = "";
            TextLastName.Text = "";
            TextPhone.Text = "";
            DeleteCustomerBtn.IsEnabled = false;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string firstName = TextFirstName.Text;
            string lastName = TextLastName.Text;
            string phone = TextPhone.Text;

            if (string.IsNullOrWhiteSpace(firstName)) 
            {
                MessageBox.Show("Пожалуйста, заполните поле 'Имя заказчика'.");
                return;
            }

            if (!IsValidPhone(phone))
            {
                MessageBox.Show("Номер телефона может содержать только цифры");
                return;
            }

            if (!IsValidInput(firstName))
            {
                MessageBox.Show("Поле 'Имя заказчика' не должно содержать специальных символов.");
                return;
            }

            Customers customer = new Customers{FirstName = firstName, LastName = lastName, Phone = phone};

            phloraEntities.Customers.Add(customer);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem != null)
            {
               Customers selected = DataGrid1.SelectedItem as Customers;

               if (selected != null)
                {
                    phloraEntities.Customers.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();

                    clearInputs();
                }            
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem != null)
            {
                Customers selected = DataGrid1.SelectedItem as Customers;
                string firstName = TextFirstName.Text;
                string lastName = TextLastName.Text;
                string Phone = TextPhone.Text;

                if (selected == null)
                {
                    MessageBox.Show("Ошибка выбора элемента.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    MessageBox.Show("Пожалуйста, заполните поле 'Название роли'.");
                    return;
                }

                if (!IsValidInput(firstName))
                {
                    MessageBox.Show("Поле 'Название роли' не должно содержать специальных символов.");
                    return;
                }

                selected.FirstName = firstName;
                selected.LastName = lastName;
                selected.Phone = Phone;
                phloraEntities.SaveChanges();
                RefreshDataGrid();
                clearInputs();
            }
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (DataGrid1.SelectedItem != null)
           {
                Customers selected = DataGrid1.SelectedItem as Customers;

                if (selected != null){
                    DeleteCustomerBtn.IsEnabled = true;
                    TextFirstName.Text = selected.FirstName;
                    TextLastName.Text = selected.LastName;
                    TextPhone.Text = selected.Phone;
                }
            }
        }

        private void TextBoxRoleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string firstName = TextFirstName.Text;
            string lastName = TextLastName.Text;
            string Phone = TextPhone.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(Phone)) {
                AddCustomerBtn.IsEnabled = false;
                EditCustomerBtn.IsEnabled = false;
            } else {
                AddCustomerBtn.IsEnabled = true;
                EditCustomerBtn.IsEnabled = true;
            }
        }
    }
}
