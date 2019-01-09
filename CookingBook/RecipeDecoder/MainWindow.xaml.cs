using RecipeDecoder.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            _model.Path = Environment.CurrentDirectory;

            DataContext = _model;
        }

        private void BtBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Yes
                    || result == System.Windows.Forms.DialogResult.OK)
                {
                    _model.Path = dialog.SelectedPath;
                }
            }
        }

        private void BtRead_Click(object sender, RoutedEventArgs e)
        {
            var items = dtFiles.SelectedItems;
            var files = new List<FileInfo>();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item is FileInfo)
                    {
                        files.Add(item as FileInfo);
                    }
                }

                _model.Read(files);
            }
        }
    }
}
