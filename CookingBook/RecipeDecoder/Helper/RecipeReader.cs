using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDecoder.Helper
{
    public class RecipeReader
    {
        private const string _RECIPE = "Zutaten:";
        private const string _WORKING = "Zubereitung:";

        public RecipeReader(string path)
        {
            Path = path;
            Recipes = new List<Recipe>();
        }

        #region Properties

        public string Path
        {
            get;
            private set;
        }

        public List<Recipe> Recipes
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Read()
        {
            try
            {
                StreamReader reader = new StreamReader(Path, Encoding.UTF8);

                List<string> lastLines = new List<string>();

                Recipe currentRecipe = null;
                RecipeVariant currentVariant = null;

                var recipeMode = false;
                var workingMode = false;

                var workingCounter = -1;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    line = line.TrimStart();
                    line = line.TrimEnd();
                    line = line.Replace("\t", " ");

                    if (lastLines.Count == 4)
                    {
                        lastLines.RemoveAt(0);
                    }

                    lastLines.Add(line);

                    if ((recipeMode 
                        || workingMode)
                        && line == string.Empty)
                    {
                        recipeMode = false;
                        workingMode = false;
                    }

                    if (!recipeMode
                        && !workingMode)
                    {
                        if (line.StartsWith(_RECIPE))
                        {
                            currentRecipe = new Recipe();
                            currentRecipe.Name = lastLines.FirstOrDefault();

                            currentVariant = new RecipeVariant();
                            currentVariant.Name = currentRecipe.Name;

                            currentRecipe.Variants.Add(currentVariant);
                            Recipes.Add(currentRecipe);
                            workingMode = false;
                            recipeMode = true;
                        }
                        else if (line.StartsWith(_WORKING))
                        {
                            reader.ReadLine();
                            recipeMode = false;
                            workingMode = true;
                            workingCounter++;

                            currentRecipe = Recipes[workingCounter];
                            currentVariant = currentRecipe.Variants.FirstOrDefault();
                        }
                    }
                    else if (recipeMode)
                    {
                        var ingredients = ParseIngredient(line);

                        foreach (var ingredient in ingredients)
                        {
                            currentVariant.AddIngredient(ingredient);
                        }
                    }
                    else if (workingMode)
                    {
                        line = line.Replace(" ", string.Empty);
                        currentVariant.Preparation += line + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                Recipes = new List<Recipe>();
            }
        }

        public List<Ingredient> ParseIngredient(string line)
        {
            var retVal = new List<Ingredient>();
            var ingredient = new Ingredient();

            try
            {
                if (line.Contains(","))
                {
                    var split = line.Split(',');
                    foreach (var ing in split)
                    {
                        var temp = ing.Trim();
                        retVal.Add(new Ingredient()
                        {
                            Name = temp
                        });
                    }
                }
                else if (line.Contains(" "))
                {
                    var split = line.Split(' ').ToList();

                    var amount = string.Empty;
                    var unit = string.Empty;
                    var hasNumber = false;
                    var numberIndex = 0;

                    var counter = 0;
                    foreach (var letter in split[0])
                    {
                        counter++;

                        if (char.IsNumber(letter))
                        {
                            numberIndex = counter;
                            hasNumber = true;
                        }
                    }

                    // Menge wurde vergeben
                    if (hasNumber)
                    {
                        amount = split[0].Substring(0, numberIndex);

                        if (counter > numberIndex)
                        {
                            unit = split[0].Substring(numberIndex);
                        }

                        ingredient.Amount = amount;
                        split.RemoveAt(0);
                    }

                    // Einheit wurde direkt hinter der Menge gelesen
                    if (hasNumber && !string.IsNullOrWhiteSpace(unit))
                    {
                        ingredient.Unit = unit;
                    }
                    else if (hasNumber && split.Count > 1)
                    {
                        unit = split[0];
                        ingredient.Unit = unit;
                        split.RemoveAt(0);
                    }

                    ingredient.Name = string.Join(" ", split);
                    retVal.Add(ingredient);
                }
                else
                {
                    ingredient.Name = line;
                    retVal.Add(ingredient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retVal;
        }

        #endregion
    }
}
