using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PasswordManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SQLquery.CreateDatabase();
        }

        private void LoginField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginField.Text == "Логин")
            {
                LoginField.Text = "";
                LoginField.Foreground = Brushes.Black;
            }
        }

        private void LoginField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginField.Text == "")
            {
                LoginField.Text = "Логин";
                LoginField.Foreground = Brushes.Gray;
            }
        }

        private void PasswordField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Password != "")
            {
                PasswordField.Password = "";
                PasswordField.Foreground = Brushes.Black;
            }
        }

        private void PasswordField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Password == "")
            {
                PasswordField.Foreground = Brushes.Gray;

                PasswordField.Password = "Пароль";
            }
        }

        private void LoginFieldR_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginFieldR.Text == "Логин")
            {
                LoginFieldR.Text = "";
                LoginFieldR.Foreground = Brushes.Black;
            }
        }

        private void LoginFieldR_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginFieldR.Text == "")
            {
                LoginFieldR.Text = "Логин";
                LoginFieldR.Foreground = Brushes.Gray;
            }
        }

        private void PasswordFieldR_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordFieldR.Password != "")
            {
                PasswordFieldR.Password = "";
                PasswordFieldR.Foreground = Brushes.Black;
            }
        }

        private void PasswordFieldR_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordFieldR.Password == "")
            {
                PasswordFieldR.Foreground = Brushes.Gray;

                PasswordFieldR.Password = "Пароль";
            }
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (SQLquery.CheckLogin(LoginField.Text))
            {
                if (SQLquery.CheckPassword(LoginField.Text, PasswordField.Password))
                {
                    MainPasswordManager mainPasswordManager = new MainPasswordManager(SQLquery.GetUserID(LoginField.Text));
                    mainPasswordManager.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введен неверный пароль!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Такой пользователь не зарегистрирован!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SQLquery.CheckLogin(LoginFieldR.Text))
            {
                if (ValidatePassword(PasswordFieldR.Password))
                {
                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Регистрация завершена", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    SQLquery.RegisterUser(LoginFieldR.Text, PasswordFieldR.Password);
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать не менее 8 символов и не более 20, должна быть хотя бы одна цифра и одна буква. В пароле используются буквы латинского алфавита.", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static bool ValidatePassword(string password)
        {
            // Проверка длины пароля
            if (password.Length < 8 || password.Length > 20)
            {
                return false;
            }

            // Проверка на наличие только латинских букв и цифр
            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            {
                return false;
            }

            // Проверка на наличие хотя бы одной буквы и одной цифры
            if (!Regex.IsMatch(password, @"[a-zA-Z]") || !Regex.IsMatch(password, @"\d"))
            {
                return false;
            }

            // Проверка на отсутствие трёх повторяющихся символов подряд
            for (int i = 0; i < password.Length - 2; i++)
            {
                if (password[i] == password[i + 1] && password[i + 1] == password[i + 2])
                {
                    return false;
                }
            }

            return true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
