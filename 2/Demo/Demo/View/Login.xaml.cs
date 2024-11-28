using Demo.Model;
using System;
using System.Windows;

namespace Demo.View
{
    public partial class Login : Window
    {
        private readonly Database _database;

        public Login()
        {
            InitializeComponent();
            _database = new Database();
        }

        // Обработчик кнопки "Войти"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = loginTextBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_database.CheckLoginFromUsers(username, password)) // Проверяем логин и пароль
            {
                MessageBox.Show("Добро пожаловать!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Открываем окно списка запросов
                List listWindow = new List();
                listWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
