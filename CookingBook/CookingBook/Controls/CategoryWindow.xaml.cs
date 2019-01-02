using CookingBook.ViewModels;
using CookingLib.Objects;
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
    /// Interaktionslogik für CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        public CategoryWindow()
        {
            InitializeComponent();
        }

        #region Ingredienants

        private void menuAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CategoryViewModel;

            if (model != null)
            {
                var category = new Category();

                model.AddCategory(category);
            }
        }

        private void dtCategories_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtCategories_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var model = DataContext as CategoryViewModel;

            if (e.Key == Key.Down && model != null)
            {
                var item = dtCategories.CurrentItem != null ? dtCategories.CurrentItem as Category : null;

                if (item != null && item == model.ClonedCategories.LastOrDefault())
                {
                    var ingredient = new Category();

                    model.AddCategory(ingredient);

                    dtCategories.CurrentItem = ingredient;
                }
            }
        }

        private void dtCategories_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Cell, true);
        }

        #endregion

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CategoryViewModel;

            if (model != null)
            {
                model.SaveCategories();
            }

            this.Close();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
