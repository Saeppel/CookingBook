using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("Ingredient", IsNullable = false)]
    public class Ingredient: ObjectBase
    {
        #region Constructor

        public Ingredient()
        {
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

        [XmlElement("Amount")]
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        private string _amount;

        [XmlElement("Unit")]
        public string Unit
        {
            get
            {
                return _unit ?? string.Empty;
            }
            set
            {
                _unit = value;
                OnPropertyChanged("Unit");
            }
        }
        private string _unit;

        
        [XmlElement("CookingType")]
        public string CookingType
        {
            get
            {
                return _cookingType ?? string.Empty;
            }
            set
            {
                _cookingType = value;
                OnPropertyChanged("CookingType");
            }
        }
        private string _cookingType;

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Name: {Name}, Amount: {Amount}, Unit: {Unit}, CookingType: {CookingType}";
        }

        #endregion
    }
}