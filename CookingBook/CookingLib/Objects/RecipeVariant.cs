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
    [XmlRootAttribute("RecipeVariant", Namespace = "", IsNullable = false)]
    public class RecipeVariant : ObjectBase
    {
        #region Constructor

        public RecipeVariant()
        {
            Ingredients = new ObservableCollection<Ingredient>();
        }

        #endregion

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

        [XmlElement("Preparation")]
        public string Preparation
        {
            get
            {
                return _preparation ?? string.Empty;
            }
            set
            {
                _preparation = value;
                OnPropertyChanged("Preparation");
            }
        }
        private string _preparation;

        [XmlElement("WorkingTime")]
        public int WorkingTime
        {
            get
            {
                return _workingTime;
            }
            set
            {
                _workingTime = value;
                OnPropertyChanged("WorkingTime");
            }
        }
        private int _workingTime;

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