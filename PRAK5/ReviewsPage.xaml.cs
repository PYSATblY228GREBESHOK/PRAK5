using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PRAK5
{
    public partial class ReviewsPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();

        public ReviewsPage()
        {
            InitializeComponent();
            InitializeRatingList();
            RefreshDataGrid();
        }

        private void InitializeRatingList()
        {
            List<int> ratings = new List<int> { 1, 2, 3, 4, 5 };
            RatingList.ItemsSource = ratings; 
        }

        private void clearInputs()
        {
            CommenT.Text = "";
            RatingList.SelectedValue = null; 
            DeleteBtnClick.IsEnabled = false;
        }

        private void RefreshDataGrid()
        {
            DataGrid3.ItemsSource = phloraEntities.Reviews.ToList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (DataGrid3.SelectedItem != null)
            {
                Reviews selected = DataGrid3.SelectedItem as Reviews;

                if (selected != null)
                {
                    phloraEntities.Reviews.Remove(selected);
                    phloraEntities.SaveChanges();
                    RefreshDataGrid();
                    clearInputs();
                }
            }
        }

        private void DataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid3.SelectedItem != null)
            {
                Reviews selected = DataGrid3.SelectedItem as Reviews;

                if (selected != null)
                {
                    DeleteBtnClick.IsEnabled = true;
                    CommenT.Text = selected.Comment;
                    RatingList.SelectedValue = selected.Rating; 

                }
            }
        }

        private void TextReviews(object sender, TextChangedEventArgs e)
        {
            string Comment = CommenT.Text;

            if (string.IsNullOrEmpty(Comment))
            {
                AddBtnClick.IsEnabled = false;
                EditBtnClick.IsEnabled = false;
            }
            else
            {
                AddBtnClick.IsEnabled = true;
                EditBtnClick.IsEnabled = true;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string Comment = CommenT.Text;
            int Rating = (int)RatingList.SelectedValue;

            Reviews reviews = new Reviews { Rating = Rating, Comment = Comment };

            phloraEntities.Reviews.Add(reviews);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (DataGrid3.SelectedItem != null)
            {
                Reviews selected = DataGrid3.SelectedItem as Reviews;
                string Comment = CommenT.Text;
                int Rating = (int)RatingList.SelectedValue;

                selected.Comment = Comment;
                selected.Rating = Rating;
                phloraEntities.SaveChanges();
                RefreshDataGrid();
                clearInputs();
            }
        }


    }
}
