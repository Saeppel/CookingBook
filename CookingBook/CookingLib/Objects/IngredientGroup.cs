using CookingLib.Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("IngredientGroup", IsNullable = false)]
    public class IngredientGroup : ObjectBase
    {
        public IngredientGroup()
        {
            Ingredients = new ObservableCollection<Ingredient>();
        }

        #region Properties

        [XmlElement("Name")]
        public string Name
        {
            get
            {
                return _name ?? string.Empty;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _name;

        [XmlArray(ElementName = "Ingredients")]
        [XmlArrayItem("Ingredient", Type = typeof(Ingredient))]
        public ObservableCollection<Ingredient> Ingredients
        {
            get
            {
                return _ingredients;
            }
            set
            {
                _ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }
        private ObservableCollection<Ingredient> _ingredients;

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"IngredientGroup: {Name}";
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (Ingredients == null)
            {
                Ingredients = new ObservableCollection<Ingredient>();
            }

            ingredient.ID = RecipeContainer.Instance.GetNewIngredientID();

            Ingredients.Add(ingredient);

            OnPropertyChanged("Ingredients");
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            if (Ingredients == null)
            {
                Ingredients = new ObservableCollection<Ingredient>();
            }

            Ingredients.Remove(ingredient);

            OnPropertyChanged("Ingredients");
        }

        #endregion
    }
}
