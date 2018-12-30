using CookingLib.Container;
using CookingLib.Objects;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBook.ViewModels
{
    public class RecipeBookViewModel : ViewModelBase
    {
        #region Constructor

        public RecipeBookViewModel()
        {
            LoadData();
        }

        #endregion

        #region Properties

        public List<string> Categories
        {
            get
            {
                return RecipeContainer.Instance.Categories;
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
            set
            {
                _recipes = value;
                OnPropertyChanged("Recipes");
            }
        }
        private ObservableCollection<Recipe> _recipes;

        public Recipe SelectedRecipe
        {
            get
            {
                return _selectedRecipe;
            }
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged("SelectedRecipe");
            }
        }
        private Recipe _selectedRecipe;

        public HashTag SelectedHashTag
        {
            get
            {
                return _selectedHashTag;
            }
            set
            {
                _selectedHashTag = value;
                OnPropertyChanged("SelectedHashTag");
            }
        }
        private HashTag _selectedHashTag;

        public string SearchValues
        {
            get
            {
                return _searchValues ?? string.Empty;
            }
            set
            {
                _searchValues = value;
                OnPropertyChanged("SearchValues");

                UpdateListByFilter();
            }
        }
        private string _searchValues;

        public string HashTagValues
        {
            get
            {
                return _hashTagValues ?? string.Empty;
            }
            set
            {
                _hashTagValues = value;
                OnPropertyChanged("SearchValues");

                UpdateListByFilter();
            }
        }
        private string _hashTagValues;

        #endregion

        #region Methods

        public void LoadData()
        {
            UpdateListByFilter();

            OnPropertyChanged("Categories");
        }

        private void UpdateListByFilter()
        {
            long id = -1;

            if (SelectedRecipe != null && SelectedRecipe.ID > 0)
            {
                id = SelectedRecipe.ID;
            }

            if (!string.IsNullOrWhiteSpace(SearchValues) || !string.IsNullOrWhiteSpace(HashTagValues))
            {
                var filteredList = new ConcurrentBag<Recipe>();

                var recipes = RecipeContainer.Instance.Recipes;

                var searchValues = SearchValues.Split(',').Select(x => x.TrimStart(' ')).ToList();
                var hashTags = HashTagValues.Split(' ').ToList();

                if (searchValues != null)
                {
                    searchValues.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                }

                if (hashTags != null)
                {
                    hashTags.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                }

                Parallel.ForEach(recipes, recipe =>
                {
                    if (recipe.Match(searchValues) ||
                        recipe.ContainsHashTag(hashTags))
                    {
                        filteredList.Add(recipe);
                    }
                });

                if (filteredList != null && filteredList.Count > 0)
                {
                    filteredList = new ConcurrentBag<Recipe>(filteredList.OrderBy(x => x.Name));
                }

                Recipes = new ObservableCollection<Recipe>(filteredList);
            }
            else
            {
                Recipes = new ObservableCollection<Recipe>(RecipeContainer.Instance.Recipes);
            }

            if (id > 0 && Recipes.Any(x => x.ID == id))
            {
                SelectedRecipe = Recipes.FirstOrDefault(x => x.ID == id);
            }
        }

        #endregion
    }
}
