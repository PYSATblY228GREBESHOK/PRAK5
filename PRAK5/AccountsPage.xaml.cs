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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRAK5
{
    /// <summary>
    /// Логика взаимодействия для AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : Page
    {
        PhloraEntities phloraEntities = new PhloraEntities();

        public AccountsPage()
        {
            InitializeComponent();
            RefreshDataGrid();

            var accessLevels = AccessLevel.GetAccessLevels();
            AccessLevelComboBox.ItemsSource = accessLevels;
            AccessLevelComboBox.DisplayMemberPath = "Role";    
            AccessLevelComboBox.SelectedValuePath = "Level";    
        }

        private void RefreshDataGrid()
        {
            AccountsDataGrid.ItemsSource = phloraEntities.Accounts.ToList();
        }

        private void clearInputs()
        {
            LoginTextBox.Text = "";
            passTextBox.Password = "";
            AccessLevelComboBox.SelectedValue = null;
        }

        private void onAddAccount(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string pass = passTextBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Не указан данные аккаунта");
                return;
            }

            if (AccessLevelComboBox.SelectedValue == null)
            {
                MessageBox.Show("Не указан уровень доступа");
                return;
            }

            int accessLevel = (int)AccessLevelComboBox.SelectedValue;
            string passHash = HashHelper.ComputeSha256Hash(pass);

            Accounts newAccount = new Accounts { Login = login, Pass = passHash, access_level = accessLevel };

            phloraEntities.Accounts.Add(newAccount);
            phloraEntities.SaveChanges();
            RefreshDataGrid();
            clearInputs();
        }

        private void onDeleteAccount(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItem != null)
            {
                Accounts selected = AccountsDataGrid.SelectedItem as Accounts;

                if (selected != null)
                {
                    try
                    {
                        phloraEntities.Accounts.Remove(selected);
                        phloraEntities.SaveChanges();
                        RefreshDataGrid();
                        clearInputs();
                }   catch (Exception ex)
                    {

                        MessageBox.Show("Для того, чтобы удалить аккаунт, необходимо сначала удалить Сотрудника, к которому этот аккаунт привязан");
                }
            }
        }
        }
    }
}
