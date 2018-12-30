using CookingLib.Container;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("Recipe", Namespace = "", IsNullable = false)]
    public class Recipe : ObjectBase
    {
        #region Constructor

        public Recipe()
        {
            Variants = new ObservableCollection<RecipeVariant>();
            HashTags = new ObservableCollection<HashTag>();
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

        [XmlElement("Number")]
        public string Number
        {
            get
            {
                return _number ?? string.Empty;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        private string _number;

        [XmlElement("Description")]
        public string Description
        {
            get
            {
                return _description ?? string.Empty;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        private string _description;

        [XmlElement("Category")]
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }
        private string _category;

        [XmlArray(ElementName = "Variants")]
        [XmlArrayItem("Variant", Type = typeof(RecipeVariant))]
        public ObservableCollection<RecipeVariant> Variants
        {
            get
            {
                return _variants;
            }
            set
            {
                _variants = value;
                OnPropertyChanged("Variants");
            }
        }
        private ObservableCollection<RecipeVariant> _variants;

        [XmlArray(ElementName = "HashTags")]
        [XmlArrayItem("HashTag", Type = typeof(HashTag))]
        public ObservableCollection<HashTag> HashTags
        {
            get
            {
                return _hashTags;
            }
            set
            {
                _hashTags = value;
                OnPropertyChanged("HashTags");
            }
        }
        private ObservableCollection<HashTag> _hashTags;

        #endregion

        #region Methods

       public void AddHashTag(HashTag hashTag)
        {
            if (HashTags == null)
            {
                HashTags = new ObservableCollection<HashTag>();
            }

            HashTags.Add(hashTag);

            OnPropertyChanged("HashTags");
        }

        public void RemoveHashTag(HashTag hashTag)
        {
            if (HashTags == null)
            {
                HashTags = new ObservableCollection<HashTag>();
            }

            HashTags.Remove(hashTag);

            OnPropertyChanged("HashTags");
        }

        public bool Match(List<string> searchValues)
        {
            var retVal = false;

            if (searchValues != null && searchValues.Count > 0)
            {
                foreach (var val in searchValues.Select(x => x.ToLower()))
                {
                    if (retVal == false &&
                        (Name.ToLower().Contains(val) ||
                        Variants.Any(x => x.Preparation.ToLower().Contains(val)) ||
                        Variants.Any(x => x.Name.ToLower().Contains(val))))
                    {
                        retVal = true;
                    }
                }
            }

            return retVal;
        }

        public bool ContainsHashTag(List<string> hashTags)
        {
            var retVal = false;

            if (hashTags != null && hashTags.Count > 0)
            {
                foreach (var val in hashTags.Select(x => x.ToLower()))
                {
                    if (retVal == false && HashTags.Any(x => x.Text.ToLower().Contains(val)))
                    {
                        retVal = true;
                    }
                }
            }
                        
            return retVal;
        }

        #endregion
    }
}
