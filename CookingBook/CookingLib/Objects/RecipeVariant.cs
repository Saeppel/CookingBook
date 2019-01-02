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
    [XmlRootAttribute("RecipeVariant", IsNullable = false)]
    public class RecipeVariant : ObjectBase
    {
        #region Constructor

        public RecipeVariant()
        {
            IngredientGroups = new ObservableCollection<IngredientGroup>();
            Preparation = string.Empty;
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

        [XmlElement("PreparingTime")]
        public int PreparingTime
        {
            get
            {
                return _preparingTime;
            }
            set
            {
                _preparingTime = value;
                OnPropertyChanged("PreparingTime");
            }
        }
        private int _preparingTime;

        [XmlElement("BakingTime")]
        public int BakingTime
        {
            get
            {
                return _bakingTime;
            }
            set
            {
                _bakingTime = value;
                OnPropertyChanged("BakingTime");
            }
        }
        private int _bakingTime;

        [XmlElement("RestTime")]
        public int RestTime
        {
            get
            {
                return _restTime;
            }
            set
            {
                _restTime = value;
                OnPropertyChanged("RestTime");
            }
        }
        private int _restTime;

        [XmlElement("Temperature")]
        public int Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                OnPropertyChanged("Temperature");
            }
        }
        private int _temperature;

        [XmlArray(ElementName = "IngredientGroups")]
        [XmlArrayItem("IngredientGroup", Type = typeof(IngredientGroup))]
        public ObservableCollection<IngredientGroup> IngredientGroups
        {
            get
            {
                return _ingredientGroups;
            }
            set
            {
                _ingredientGroups = value;
                OnPropertyChanged("IngredientGroups");
            }
        }
        private ObservableCollection<IngredientGroup> _ingredientGroups;

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Variant: {Name}, Preparation: {Preparation}, WorkingTime: {WorkingTime}, PreparingTime: {PreparingTime}, BakingTime: {BakingTime}, RestTime: {RestTime}, Temperature: {Temperature}";
        }

        public void AddIngredientGroup(IngredientGroup group)
        {
            if (IngredientGroups == null)
            {
                IngredientGroups = new ObservableCollection<IngredientGroup>();
            }

            group.ID = RecipeContainer.Instance.GetNewIngredientID();

            IngredientGroups.Add(group);

            OnPropertyChanged("IngredientGroups");
        }

        public void RemoveIngredientGroup(IngredientGroup group)
        {
            if (IngredientGroups == null)
            {
                IngredientGroups = new ObservableCollection<IngredientGroup>();
            }

            IngredientGroups.Remove(group);

            OnPropertyChanged("IngredientGroups");
        }

        #endregion
    }
}