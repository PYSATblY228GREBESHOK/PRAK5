using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PRAK5
{
    public partial class ProductsPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();

        public ProductsPage()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void clearInputs()
        {
            NamE.Text = "";
            DescriptioN.Text = "";
            PricE.Text = "";
            DeleteButtonClick.IsEnabled = false;
        }

        private void RefreshDataGrid()
        {
            DataGrid4.ItemsSource = phloraEntities.Products.ToList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid4.SelectedItem != null)
            {
                Products selected = DataGrid4.SelectedItem as Products;

                if (selected != null)
                {
                    phloraEntities.Products.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();
                    clearInputs();
                }
            }
        }

        private void DataGrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid4.SelectedItem != null)
            {
                Products selected = DataGrid4.SelectedItem as Products;

                if (selected != null)
                {
                    DeleteButtonClick.IsEnabled = true;
                    NamE.Text = selected.Name;
                    DescriptioN.Text = selected.Description;
                    PricE.Text = selected.Price.ToString(); 
                }
            }
        }


        private void TextProducts(object sender, TextChangedEventArgs e)
        {
            string name = NamE.Text;
            string description = DescriptioN.Text;
            string price = PricE.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(price))
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
                string Name = NamE.Text;
                string Description = DescriptioN.Text;
                decimal Price;

                if (decimal.TryParse(PricE.Text, out Price))
                {
                    Products products = new Products { Name = Name, Description = Description, Price = Price };

                    phloraEntities.Products.Add(products);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();
                    clearInputs();
                }
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid4.SelectedItem != null)
            {
                Products selected = DataGrid4.SelectedItem as Products;
                string Name = NamE.Text;
                string Description = DescriptioN.Text;
                decimal Price;

                if (decimal.TryParse(PricE.Text, out Price))
                {
                    selected.Name = Name;
                    selected.Description = Description;
                    selected.Price = Price;
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();
                    clearInputs();
                }
            }
        }
    }
}
