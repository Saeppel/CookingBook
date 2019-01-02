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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für IngredientGrid.xaml
    /// </summary>
    public partial class IngredientView : UserControl
    {
        public IngredientView()
        {
            InitializeComponent();
        }

        #region Ingredienants

        private void menuAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as IngredientGroupViewModel;

            if (model != null)
            {
                var ingredient = new Ingredient();

                model.Group.AddIngredient(ingredient);
            }
        }

        private void menuRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as IngredientGroupViewModel;

            var item = dtIngredients.CurrentItem != null ? dtIngredients.CurrentItem as Ingredient : null;

            if (model != null && item != null)
            {
                model.Group.RemoveIngredient(item);
            }
        }

        private void dtIngredients_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtIngredients_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var model = DataContext as IngredientGroupViewModel;

            if (e.Key == Key.Down && model != null)
            {
                var item = dtIngredients.CurrentItem != null ? dtIngredients.CurrentItem as Ingredient : null;

                if (item != null && item == model.Group.Ingredients.LastOrDefault())
                {
                    var ingredient = new Ingredient();

                    model.Group.AddIngredient(ingredient);

                    dtIngredients.CurrentItem = ingredient;
                }
            }
        }

        private void dtIngredients_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Cell, true);
        }

        #endregion
    }
}
