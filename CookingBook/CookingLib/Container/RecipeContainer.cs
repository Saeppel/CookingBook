using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CookingLib.Container
{
    public class RecipeContainer
    {
        #region Fields

        private static RecipeContainer _instance;
        private List<Recipe> _recipes;
        private string _recipeDirectory = typeof(Recipe).Name;

        #endregion

        #region Constructor

        private RecipeContainer()
        {
            _recipes = new List<Recipe>();

            _categories = new List<string>()
            {
                //new Category(1, "Salat"),
                //new Category(2, "Kuchen"),
                //new Category(3, "Vegetarisch"),
                //new Category(4, "FLEISCH")
                "Salat",
                "Kuchen",
                "Vegetarisch",
                "FLEISCH",
            };

            LoadAllRecipes();
        }

        #endregion

        #region Properties

        public static RecipeContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecipeContainer();
                }

                return _instance;
            }
        }

        public List<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
        }

        public List<string> Categories
        {
            get
            {
                return _categories;
            }
        }
        private List<string> _categories;

        #endregion

        #region Methods

        public void LoadAllRecipes()
        {
            var directory = new DirectoryInfo(Path.Combine(System.Environment.CurrentDirectory, _recipeDirectory));
            if (directory.Exists)
            {
                foreach (var file in directory.GetFiles("*.srf"))
                {
                    var recipe = Recipe.Load<Recipe>(file.Name);

                    if (recipe != null)
                    {
                        _recipes.Add(recipe);
                    }
                }
            }
        }

        public void AddNewRecipe(Recipe recipe)
        {
            var now = DateTime.Now;

            var tempID = string.Format("{0}{1}{2}{4}{5}[6}[7}", now.Year, now.Month, now.Date, now.Hour, now.Minute, now.Second, now.Millisecond);

            long id = Convert.ToInt64(tempID);
            recipe.ID = id;

            Recipe.Save(recipe, recipe.ID);

            if (!_recipes.Any(x => x.ID == recipe.ID))
            {
                _recipes.Add(recipe);
            }
        }

        public void UpdateRecipe(Recipe recipe)
        {
            Recipe.Save(recipe, recipe.ID);

            if (_recipes.Any(x => x.ID == recipe.ID))
            {
                var update = GetRecipe(recipe.ID);
            }
            else
            {
                _recipes.Add(recipe);
            }
        }

        public void DeleteRecipe(Recipe recipe)
        {
            Recipe.Delete(recipe);

            Recipes.RemoveAll(x => x.ID == recipe.ID);
        }

        public Recipe GetRecipe(long id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }

        public List<Recipe> GetRecipes(List<long> recipeIDs)
        {
            return recipeIDs != null ? _recipes.Where(x => recipeIDs.Contains(x.ID)).ToList() : new List<Recipe>();
        }

        public List<string> GetAllHashTags()
        {
            var hashTags = new List<string>();

            foreach (var tags in Recipes.Select(x => x.HashTags))
            {
                foreach (var tag in tags)
                {
                    if (!hashTags.Contains(tag.Text))
                    {
                        hashTags.Add(tag.Text);
                    }
                }
            }

            return hashTags;
        }

        public long GetNewIngredientID()
        {
            long id = 1;

            if(Recipes != null && Recipes.Count > 0)
            {
                var found = false;

                foreach (var recipe in Recipes)
                {
                    foreach (var ingredient in recipe.Variants)
                    {
                        if (ingredient.ID > id)
                        {
                            found = true;
                            id = ingredient.ID;
                        }
                    }
                }

                if (found)
                {
                    id += 1;
                }
            }            

            return id;
        }

        #endregion
    }
}
