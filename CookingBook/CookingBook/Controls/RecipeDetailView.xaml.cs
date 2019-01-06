using CookingBook.ViewModels;
using CookingLib.Objects;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        #region Utilities

        private void MenuAddUtility_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            if (model != null)
            {
                var Utility = new Utility();

                model.Recipe.AddUtility(Utility);
            }
        }

        private void MenuRemoveUtility_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            var item = dtUtilities.CurrentItem != null ? dtUtilities.CurrentItem as Utility : null;

            if (model != null && item != null)
            {
                model.Recipe.RemoveUtility(item);
            }
        }

        private void DtUtilities_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DtUtilities_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            if (e.Key == Key.Down && model != null)
            {
                var item = dtUtilities.CurrentItem != null ? dtUtilities.CurrentItem as Utility : null;

                if (item != null && item == model.Recipe.Utilities.LastOrDefault())
                {
                    var Utility = new Utility();

                    model.Recipe.AddUtility(Utility);

                    dtUtilities.CurrentItem = Utility;
                }
            }
        }

        private void DtUtilities_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Cell, true);
        }

        #endregion

        #region Properties

        public RecipeVariantViewModel ViewModel
        {
            get
            {
                return DataContext as RecipeVariantViewModel;
            }
        }

        #endregion

        #region Methods

        public void CreateIngredientViews()
        {
            var model = DataContext as RecipeVariantViewModel;

            if (model != null)
            {
                ingredientTabControl.Items.Clear();

                if (model.Variant != null)
                {
                    foreach (var group in model.Variant.IngredientGroups)
                    {
                        var view = new IngredientView();
                        var viewModel = new IngredientGroupViewModel()
                        {
                            Variant = model.Variant,
                            Group = group,
                        };

                        view.DataContext = viewModel;

                        var tabItem = new TabItem();
                        var binding = new Binding("Name");
                        binding.Source = group;
                        BindingOperations.SetBinding(tabItem, TabItem.HeaderProperty, binding);

                        tabItem.Content = view;

                        ingredientTabControl.Items.Add(tabItem);
                    }

                    if (ingredientTabControl.Items.Count > 0)
                    {
                        ingredientTabControl.SelectedItem = ingredientTabControl.Items[0];
                    }
                }
            }
        }

        #endregion

        private void BtNewGroup_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            if (model != null && model.Variant != null)
            {
                var group = new IngredientGroup()
                {
                    Name = $"Gruppe {model.Variant.IngredientGroups.Count + 1}"
                };

                var view = new IngredientView();
                var viewModel = new IngredientGroupViewModel()
                {
                    Variant = model.Variant,
                    Group = group,
                };

                model.Variant.IngredientGroups.Add(group);

                view.DataContext = viewModel;

                var tabItem = new TabItem();
                var binding = new Binding("Name");
                binding.Source = group;
                BindingOperations.SetBinding(tabItem, TabItem.HeaderProperty, binding);

                tabItem.Content = view;

                ingredientTabControl.Items.Add(tabItem);

                ingredientTabControl.SelectedItem = ingredientTabControl.Items[ingredientTabControl.Items.Count - 1];
            }
        }

        private void BtDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as RecipeVariantViewModel;

            if (model != null && model.Variant != null && ingredientTabControl.SelectedItem != null)
            {
                var view = ((TabItem)ingredientTabControl.SelectedItem).Content as IngredientView;

                if (view != null && view.ViewModel != null)
                {
                    var group = view.ViewModel.Group;

                    model.Variant.RemoveIngredientGroup(group);
                    ingredientTabControl.Items.Remove(ingredientTabControl.SelectedItem);
                }
            }
        }
    }
}
