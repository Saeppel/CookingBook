using CookingBook.ViewModels;
using CookingLib.Objects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            this.ViewModel = new RecipeBookViewModel();
        }

        #region Properties

        public RecipeBookViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            private set
            {
                _viewModel = value;

                this.DataContext = _viewModel;
            }
        }
        private RecipeBookViewModel _viewModel;

        #endregion

        #region Events

        private void dtRecipes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.SelectedRecipe != null)
            {
                CreateRecipeView(ViewModel.SelectedRecipe);
            }
        }

        private void menuAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe();

            var variant = new RecipeVariant();

            recipe.Variants.Add(variant);

            CreateRecipeView(recipe);
        }

        private void menuEditRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedRecipe != null)
            {
                CreateRecipeView(ViewModel.SelectedRecipe);
            }
        }        

        private void btNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe();

            var variant = new RecipeVariant();

            recipe.Variants.Add(variant);

            CreateRecipeView(recipe);
        }

        private void btRandom_Click(object sender, RoutedEventArgs e)
        {
            var count = ViewModel.Recipes.Count;

            Random rnd = new Random();
            int number = rnd.Next(0, count);

            var recipe = ViewModel.Recipes[number];

            ViewModel.SelectedRecipe = recipe;

            CreateRecipeView(recipe);
        }

        #endregion

        #region Methods

        public void CreateRecipeView(Recipe recipe)
        {
            ViewModel.SelectedRecipe = recipe;
            RecipeContent.Visibility = Visibility.Visible;
            RecipeContent.CreateVariantViews();
        }

        #endregion

        private void dtRecipes_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void dtRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel.SelectedRecipe == null)
            {
                RecipeContent.CreateVariantViews();
                RecipeContent.Visibility = Visibility.Collapsed;
            }
        }
    }
}