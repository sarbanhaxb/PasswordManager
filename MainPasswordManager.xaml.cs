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
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Логика взаимодействия для MainPasswordManager.xaml
    /// </summary>
    public partial class MainPasswordManager : Window
    {
        private int userID;
        List <Item> items;
        public MainPasswordManager(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            items = SQLquery.GetItems(userID);
            ListData.ItemsSource = SQLquery.GetItems(userID);
            LoginField.IsEnabled = false;
            PasswordField.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddObject addObject = new AddObject(userID);
            addObject.ShowDialog();
            ListData.ItemsSource = SQLquery.GetItems(userID);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLquery.DeleteObj(ListData.SelectedItem.ToString(), userID);
            ListData.ItemsSource = SQLquery.GetItems(userID);
        }

        private void ListData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            items = SQLquery.GetItems(userID);
            LoginField.IsEnabled = true;
            LoginField.Foreground = Brushes.Black;
            PasswordField.IsEnabled = true;
            PasswordField.Foreground = Brushes.Black;


            if (ListData.SelectedItem != null)
            {
                foreach (Item item in items)
                {
                    if (item.Title == ListData.SelectedItem.ToString())
                    {
                        LoginField.Text = item.Login;
                        PasswordField.Password = item.Password;
                        PasswordFieldVisible.Text = item.Password;

                        PasswordField.Visibility = Visibility.Visible;
                        PasswordFieldVisible.Visibility = Visibility.Hidden;
                        LabelVisib.Text = "Показать";
                        checkBox.IsChecked = false;
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordField.Visibility = Visibility.Hidden;
            PasswordFieldVisible.Visibility = Visibility.Visible;
            LabelVisib.Text = "Спрятать";
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordField.Visibility = Visibility.Visible;
            PasswordFieldVisible.Visibility = Visibility.Hidden;
            LabelVisib.Text = "Показать";
        }
    }
}
