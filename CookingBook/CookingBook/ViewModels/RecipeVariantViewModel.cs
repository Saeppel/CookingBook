using CookingLib.Objects;

namespace CookingBook.ViewModels
{
    public class RecipeVariantViewModel : ViewModelBase
    {
        public RecipeVariantViewModel()
        {

        }

        #region Properties

        public Recipe Recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                _recipe = value;
                OnPropertyChanged("Recipe");
            }
        }
        private Recipe _recipe;

        public RecipeVariant Variant
        {
            get
            {
                return _variant;
            }
            set
            {
                _variant = value;
                OnPropertyChanged("Variant");
            }
        }
        private RecipeVariant _variant;

        public Ingredient SelectedIngredient
        {
            get
            {
                return _selectedIngredient;
            }
            set
            {
                _selectedIngredient = value;
                OnPropertyChanged("SelectedIngredient");
            }
        }
        private Ingredient _selectedIngredient;

        public Utility SelectedUtility
        {
            get
            {
                return _selectedUtility;
            }
            set
            {
                _selectedUtility = value;
                OnPropertyChanged("SelectedUtility");
            }
        }
        private Utility _selectedUtility;

        #endregion
    }
}
