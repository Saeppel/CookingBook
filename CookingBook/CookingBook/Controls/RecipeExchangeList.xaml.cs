using CookingBook.ViewModels;
using CookingLib.Objects;
using System.Collections.Generic;
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
            var model = new RecipeExchangeListViewModel();
            DataContext = model;
        }

        private void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void BtUpload_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeExchangeListViewModel;

            if (model != null)
            {
                var recipes = new List<Recipe>();

                if (dtLocalRecipes.SelectedItems != null)
                {
                    foreach (var recipe in dtLocalRecipes.SelectedItems)
                    {
                        recipes.Add(recipe as Recipe);
                    }
                }

                model.UploadRecipes(recipes);
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeExchangeListViewModel;

            if (model != null)
            {
                var recipes = new List<Recipe>();

                if (dtPublicRecipes.SelectedItems != null)
                {
                    foreach (var recipe in dtPublicRecipes.SelectedItems)
                    {
                        recipes.Add(recipe as Recipe);
                    }
                }

                model.SaveRecipes(recipes);
            }
        }
    }
}
