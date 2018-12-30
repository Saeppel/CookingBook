using CookingBook.ViewModels;
using CookingLib.Objects;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für RecipeDetailView.xaml
    /// </summary>
    public partial class RecipeDetailView : UserControl
    {
        public RecipeDetailView()
        {
            InitializeComponent();
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Informiert über eine Propertyänderung
        /// </summary>
        /// <param name="property"></param>
        protected virtual void OnPropertyChanged(string property)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion

        private void menuAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            if (model != null)
            {
                var ingredient = new Ingredient();

                model.Variant.AddIngredient(ingredient);
            }
        }

        private void menuRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            var item = dtIngredients.CurrentItem != null ? dtIngredients.CurrentItem as Ingredient : null;

            if (model != null && item != null)
            {
                model.Variant.RemoveIngredient(item);
            }
        }

        private void dtIngredients_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dtIngredients_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            if (e.Key == Key.Down && model != null)
            {
                var item = dtIngredients.CurrentItem != null ? dtIngredients.CurrentItem as Ingredient : null;

                if (item != null && item == model.Variant.Ingredients.LastOrDefault())
                {
                    var ingredient = new Ingredient();

                    model.Variant.AddIngredient(ingredient);

                    dtIngredients.CurrentItem = ingredient;
                }                
            }
        }

        private void dtIngredients_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Cell, true);
        }
    }
}
