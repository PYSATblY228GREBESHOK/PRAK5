using PRAK5.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

    public partial class MainWindow : Window
    {
        AccountsTableAdapter adapter = new AccountsTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Manager managerWindow = new Manager();
            //managerWindow.Show();
            //this.Close();


            string login = loginText.Text;
            string pass = passText.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            // Получаем данные об аккаунте
            var account = adapter.GetDataBy(login);

            //Если есть такой аккаунт
            if (account.Rows.Count > 0)
            {
                //Sha256
                string passHash = HashHelper.ComputeSha256Hash(pass);

                if (HashHelper.VerifyHash(account.Rows[0]["pass"].ToString(), passHash))
                {
                    // Сохраняем данные об аккаунте в сессии
                    Session.Login = login;
                    //Session.PasswordHash = passHash;
                    Session.AccessLevel = Convert.ToInt32(account.Rows[0]["access_level"]);

                    Manager managerWindow = new Manager();
                    managerWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверные данные для входа");
                }

            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует");
            }

        }

        private void passText_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша Enter
            if (e.Key == Key.Enter)
            {
                // Имитация нажатия на кнопку
                Button_Click(LoginBtn, null); // Вызываем обработчик нажатия кнопки
                e.Handled = true; // Отменяем дальнейшую обработку события
            }
        }
    }
}
