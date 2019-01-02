using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.ViewModels
{
    public class IngredientGroupViewModel : ViewModelBase
    {
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

        public IngredientGroup Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
                OnPropertyChanged("Group");
            }
        }
        private IngredientGroup _group;

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
