using CookingBook.Controls;
using CookingBook.ViewModels;
using CookingLib.Helper;
using System.Windows;

namespace CookingBook
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool authorized = false;

        public MainWindow()
        {
            InitializeComponent();

            importData.IsEnabled = authorized;
            exportData.IsEnabled = authorized;
            publicList.IsEnabled = authorized;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string user = ConfigHelper.Instance.Username;
            string password = ConfigHelper.Instance.Password;

            if (string.IsNullOrWhiteSpace(user)
                || string.IsNullOrWhiteSpace(password))
            {
                var wnd = new LoginWindow();
                wnd.ShowDialog();

                user = ConfigHelper.Instance.Username;
                password = ConfigHelper.Instance.Password;
            }

            if (authorized)
            {
                FTPHelper.KillInstance();
                authorized = false;
            }
            else
            {
                FTPHelper.CreateInstance(user, password);
                authorized = FTPHelper.HasInstance;
            }

            login.Label = authorized 
                ? "Abmelden" 
                : "Anmelden";

            importData.IsEnabled = authorized;
            exportData.IsEnabled = authorized;
            publicList.IsEnabled = authorized;
        }

        private void ImportData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PublicList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new CategoryViewModel();
            var wnd = new CategoryWindow()
            {
                DataContext = viewModel
            };

            wnd.ShowDialog();
        }
    }
}
