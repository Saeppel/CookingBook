using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CookingLib.Container
{
    public class RecipeContainer : ContainerBase<Recipe>
    {
        #region Fields

        private static RecipeContainer _instance;

        #endregion

        #region Constructor

        private RecipeContainer()
        {
            LoadAllEntities();
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
                return Entities;
            }
        }

        #endregion

        #region Methods

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

        public long GetNewIngredientGroupID()
        {
            long id = 1;

            if (Recipes != null && Recipes.Count > 0)
            {
                var found = false;

                foreach (var recipe in Recipes)
                {
                    foreach (var variant in recipe.Variants)
                    {
                        foreach (var group in variant.IngredientGroups)
                        {
                            if (group.ID > id)
                            {
                                found = true;
                                id = group.ID;
                            }
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

        public long GetNewIngredientID()
        {
            long id = 1;

            if(Recipes != null && Recipes.Count > 0)
            {
                var found = false;

                foreach (var recipe in Recipes)
                {
                    foreach (var variant in recipe.Variants)
                    {
                        foreach (var group in variant.IngredientGroups)
                        {
                            foreach (var ingredient in group.Ingredients)
                            {
                                if (ingredient.ID > id)
                                {
                                    found = true;
                                    id = ingredient.ID;
                                }
                            }
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

        public long GetNewHashTagID()
        {
            long id = 1;

            if (Recipes != null && Recipes.Count > 0)
            {
                var found = false;

                foreach (var recipe in Recipes)
                {
                    foreach (var hashTag in recipe.HashTags)
                    {
                        if (hashTag.ID > id)
                        {
                            found = true;
                            id = hashTag.ID;
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

        public long GetNewUtilityID()
        {
            long id = 1;

            if (Recipes != null && Recipes.Count > 0)
            {
                var found = false;

                foreach (var recipe in Recipes)
                {
                    foreach (var utility in recipe.Utilities)
                    {
                        if (utility.ID > id)
                        {
                            found = true;
                            id = utility.ID;
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
