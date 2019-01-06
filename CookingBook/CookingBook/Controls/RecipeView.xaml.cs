using CookingBook.Helper;
using CookingBook.ViewModels;
using CookingLib.Container;
using CookingLib.Objects;
using CookingLib.ReportData;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CookingBook.Controls
{
    /// <summary>
    /// Interaktionslogik für RecipeView.xaml
    /// </summary>
    public partial class RecipeView : UserControl
    {
        public RecipeView()
        {
            InitializeComponent();
        }

        public RecipeView(RecipeBookViewModel model)
        {
            InitializeComponent();

            DataContext = model;
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

        #region Methods

        public void CreateVariantViews()
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null)
            {
                variantTabControl.Items.Clear();

                if (model.SelectedRecipe != null)
                {
                    foreach (var variant in model.SelectedRecipe.Variants)
                    {
                        var view = new RecipeDetailView();
                        var viewModel = new RecipeVariantViewModel()
                        {
                            Variant = variant,
                            Recipe = model.SelectedRecipe
                        };

                        view.DataContext = viewModel;

                        var tabItem = new TabItem();
                        var binding = new Binding("Name");
                        binding.Source = variant;
                        BindingOperations.SetBinding(tabItem, TabItem.HeaderProperty, binding);

                        tabItem.Content = view;

                        variantTabControl.Items.Add(tabItem);

                        view.CreateIngredientViews();
                    }

                    if (variantTabControl.Items.Count > 0)
                    {
                        variantTabControl.SelectedItem = variantTabControl.Items[0];
                    }
                }
            }
        }

        public void AddNewVariant()
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null)
            {
                var variant = new RecipeVariant()
                {
                    Name = $"Variante {model.SelectedRecipe.Variants.Count + 1}"
                };

                if (model.SelectedRecipe.Variants.Count > 0)
                {
                    var result = MessageBox.Show("Sollen die Daten aus der ersten Variante übernommen werden?",
                                                 "Datenübernahme",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var first = model.SelectedRecipe.Variants.FirstOrDefault();

                        foreach (var group in first.IngredientGroups)
                        {
                            var ingredientGroup = new IngredientGroup()
                            {
                                Name = group.Name
                            };

                            foreach (var ingredient in group.Ingredients)
                            {
                                var newIngredient = new Ingredient()
                                {
                                    Amount = ingredient.Amount,
                                    Name = ingredient.Name,
                                    Unit = ingredient.Unit,
                                    CookingType = ingredient.CookingType
                                };

                                ingredientGroup.AddIngredient(newIngredient);
                            }

                            variant.AddIngredientGroup(ingredientGroup);
                        }

                        variant.Name = first.Name;
                        variant.Preparation = first.Preparation;
                        variant.WorkingTime = first.WorkingTime;
                        variant.BakingTime = first.BakingTime;
                        variant.PreparingTime = first.PreparingTime;
                        variant.RestTime = first.RestTime;
                        variant.Temperature = first.Temperature;
                    }
                }
                else
                {
                    var group = new IngredientGroup()
                    {
                        Name = "Zutaten"
                    };

                    variant.AddIngredientGroup(group);
                }

                model.SelectedRecipe.Variants.Add(variant);

                var view = new RecipeDetailView();
                var viewModel = new RecipeVariantViewModel()
                {
                    Variant = variant,
                    Recipe = model.SelectedRecipe
                };

                view.DataContext = viewModel;

                var tabItem = new TabItem();
                var binding = new Binding("Name");
                binding.Source = variant;
                BindingOperations.SetBinding(tabItem, TabItem.HeaderProperty, binding);
                tabItem.Content = view;

                variantTabControl.Items.Add(tabItem);

                view.CreateIngredientViews();
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null)
            {
                if (model.SelectedRecipe.ID == 0)
                {
                    RecipeContainer.Instance.AddNewEntity(model.SelectedRecipe);
                }
                else
                {
                    RecipeContainer.Instance.UpdateEntity(model.SelectedRecipe);
                }

                model.LoadData();

                CreateVariantViews();
            }
        }

        private void btNewVariant_Click(object sender, RoutedEventArgs e)
        {
            AddNewVariant();
        }

        #endregion

        private void menuAddHashTag_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null)
            {
                var hashTag = new HashTag();
                model.SelectedRecipe.AddHashTag(hashTag);
            }
        }

        private void menuRemoveHashTag_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null)
            {
                model.SelectedRecipe.RemoveHashTag(model.SelectedHashTag);
            }
        }

        private void btRemoveVariant_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null && variantTabControl.SelectedItem != null)
            {
                var tab = (TabItem)variantTabControl.SelectedItem;

                var view = tab.Content as RecipeDetailView;

                if (view != null && view.ViewModel != null)
                {
                    model.SelectedRecipe.Variants.Remove(view.ViewModel.Variant);
                    variantTabControl.Items.Remove(tab);
                }
            }
        }

        private void dtIngredients_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Cell, true);
        }

        private void BtPrint_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeBookViewModel;

            if (model != null && variantTabControl.SelectedItem != null)
            {
                var item = variantTabControl.SelectedItem;
                var view = ((TabItem)item).Content as RecipeDetailView;

                if (view != null && view.ViewModel != null)
                {
                    var reportData = new ReportData(model.SelectedRecipe, view.ViewModel.Variant);

                    ReportHelper.CallReport(reportData);
                }
            }
        }
    }
}
