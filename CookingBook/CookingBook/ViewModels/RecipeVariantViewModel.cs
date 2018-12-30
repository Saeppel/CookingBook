using CookingLib.Objects;

namespace CookingBook.ViewModels
{
    public class RecipeVariantViewModel : ViewModelBase
    {
        public RecipeVariantViewModel()
        {

        }

        #region Properties

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

        #endregion
    }
}
