using CookingLib.Helper;
using CookingLib.Objects;
using System.Collections.Generic;

namespace CookingBook.ViewModels
{
    public class RecipeVariantViewModel : ViewModelBase
    {
        public RecipeVariantViewModel()
        {
            OvenSettings = OvenHelper.Instance.OvenSettings;
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

        public List<OvenSetting> OvenSettings
        {
            get
            {
                return _ovenSettings;
            }
            set
            {
                _ovenSettings = value;
                OnPropertyChanged("OvenSettings");
            }
        }
        private List<OvenSetting> _ovenSettings;

        #endregion
    }
}
