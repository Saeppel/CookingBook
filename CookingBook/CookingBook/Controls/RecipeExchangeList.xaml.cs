using System.Windows;
using System.Windows.Controls;

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für RecipeView.xaml
    /// </summary>
    public partial class RecipeExchangeList : UserControl
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
    }
}
