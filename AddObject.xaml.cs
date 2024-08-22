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
    /// Логика взаимодействия для AddObject.xaml
    /// </summary>
    public partial class AddObject : Window
    {
        private int userID;
        public AddObject(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text != "")
            {
                SQLquery.InsertNewToObj(userID, TitleTextBox.Text, LoginTextBox.Text, PasswordTextBox.Password);
                this.Close ();
            }
        }
    }
}
