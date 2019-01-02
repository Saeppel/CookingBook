using CookingBook.ViewModels;
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

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            var model = new LoginViewModel();
            DataContext = model;
            pwBox.Password = model.Password;
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as LoginViewModel;
            var success = false;

            if (model != null)
            {
                model.Password = pwBox.Password;
                success = model.CheckLogin();

                if (success)
                {
                    if (model.Remember)
                    {
                        model.SaveLogin();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Die Anmeldedaten sind ungültig!", 
                                    "Anmeldung", 
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Error);
                }
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as LoginViewModel;

            if (model != null)
            {
                this.Close();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtLogin_Click(this, new RoutedEventArgs());
            }
        }

        private void PwBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtLogin_Click(this, new RoutedEventArgs());
            }
        }
    }
}
