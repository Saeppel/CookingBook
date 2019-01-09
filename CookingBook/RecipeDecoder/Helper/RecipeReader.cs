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
        private const string _PREPARING = "Vorbereitungszeit:";
        private const string _RESTING = "Ruhezeit:";
        private const string _BACKING = "Backzeit:";
        private const string _TEMPERATURE = "Temperatur:";

        public RecipeReader()
        {
            Recipes = new List<Recipe>();
        }

        #region Properties

        public List<Recipe> Recipes
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Read(string file)
        {
            try
            {
                StreamReader reader = new StreamReader(file, Encoding.Default);

                List<string> lastLines = new List<string>();

                Recipe currentRecipe = null;
                RecipeVariant currentVariant = null;
                IngredientGroup currentGroup = null;

                var recipeMode = false;
                var workingMode = false;

                var workingCounter = -1;
                var ingredientCounter = 0;

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
                        && (line.StartsWith(_RECIPE)
                           || line.StartsWith(_WORKING)))
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

                            currentGroup = new IngredientGroup();
                            currentVariant.AddIngredientGroup(currentGroup);

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
                        if (line.StartsWith(_PREPARING))
                        {
                            var prep = line.Replace(_PREPARING, string.Empty).Trim();
                            if (int.TryParse(prep, out int prepTime))
                            {
                                currentVariant.PreparingTime = prepTime;
                            }
                        }
                        else if (line.StartsWith(_RESTING))
                        {
                            var rest = line.Replace(_RESTING, string.Empty).Trim();
                            if (int.TryParse(rest, out int restTime))
                            {
                                currentVariant.PreparingTime = restTime;
                            }
                        }
                        else if (line.StartsWith(_BACKING))
                        {
                            var back = line.Replace(_BACKING, string.Empty).Trim();
                            if (int.TryParse(back, out int backTime))
                            {
                                currentVariant.PreparingTime = backTime;
                            }
                        }
                        else if (line.StartsWith(_TEMPERATURE))
                        {
                            var temp = line.Replace(_TEMPERATURE, string.Empty).Trim();
                            if (int.TryParse(temp, out int temperature))
                            {
                                currentVariant.PreparingTime = temperature;
                            }
                        }
                        else
                        {
                            ingredientCounter++;
                            if (ingredientCounter == 2)
                            {
                                var unit = lastLines[2];
                                var ingr = lastLines[3];

                                line = $"{unit} {ingr}";

                                if (line.Trim() != string.Empty)
                                {
                                    var ingredients = ParseIngredient(line);

                                    foreach (var ingredient in ingredients)
                                    {
                                        currentGroup.AddIngredient(ingredient);
                                    }
                                }

                                ingredientCounter = 0;
                            }
                        }
                    }
                    else if (workingMode)
                    {
                        line = line.Replace("?", string.Empty);
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
