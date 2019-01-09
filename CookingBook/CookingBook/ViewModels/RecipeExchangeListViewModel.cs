using CookingLib.Container;
using CookingLib.Helper;
using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.ViewModels
{
    public class RecipeExchangeListViewModel : ViewModelBase
    {
        #region Constructor

        public RecipeExchangeListViewModel()
        {
            var categories = CategoryContainer.Instance.DownloadData();

            foreach (var category in categories)
            {
                CategoryContainer.Instance.UpdateEntity(category);
            }

            LocalRecipes = new ObservableCollection<Recipe>(RecipeContainer.Instance.Recipes);
            PublicRecipes = new ObservableCollection<Recipe>(RecipeContainer.Instance.DownloadData());
        }

        #endregion

        #region Properties

        public ObservableCollection<Recipe> LocalRecipes
        {
            get
            {
                return _localRecipes;
            }
            set
            {
                _localRecipes = value;
                OnPropertyChanged("LocalRecipes");
            }
        }
        private ObservableCollection<Recipe> _localRecipes;

        public ObservableCollection<Recipe> PublicRecipes
        {
            get
            {
                return _publicRecipes;
            }
            set
            {
                _publicRecipes = value;
                OnPropertyChanged("PublicRecipes");
            }
        }
        private ObservableCollection<Recipe> _publicRecipes;

        #endregion

        #region Methods

        public void SaveRecipes(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                RecipeContainer.Instance.UpdateEntity(recipe);
                LocalRecipes.Add(recipe);
            }

            OnPropertyChanged("LocalRecipes");
        }

        public void UploadRecipes(List<Recipe> recipes)
        {
            var ids = recipes.Select(x => x.ID).ToList();

            RecipeContainer.Instance.UploadData(ids);

            foreach (var recipe in recipes)
            {
                PublicRecipes.Add(recipe);
            }

            OnPropertyChanged("PublicRecipes");
        }

        #endregion
    }
}
