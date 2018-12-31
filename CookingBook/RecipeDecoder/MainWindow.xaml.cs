using RecipeDecoder.ViewModels;
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

namespace RecipeDecoder
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _model;

        public MainWindow()
        {
            InitializeComponent();

            _model = new MainViewModel();

            _model.Path = @"E:\C#Programme\CookingBook\Docs\263-264.txt";

            DataContext = _model;
        }

        private void BtOpen_Click(object sender, RoutedEventArgs e)
        {
            _model.Read();
        }
    }
}
