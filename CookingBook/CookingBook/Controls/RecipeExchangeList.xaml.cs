using System.Windows;
using System.Windows.Controls;

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für RecipeView.xaml
    /// </summary>
    public partial class RecipeExchangeList : Window
    {
        public RecipeExchangeList()
        {
            InitializeComponent();
        }

        private void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void BtUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtDownload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
