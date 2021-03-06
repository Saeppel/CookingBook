﻿using CookingLib.Container;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("Recipe", IsNullable = false)]
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

        [XmlElement("CategoryID")]
        public long CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
                OnPropertyChanged("CategoryID");
            }
        }
        private long _categoryID;

        [XmlElement("InfoText")]
        public string InfoText
        {
            get
            {
                return _infoText;
            }
            set
            {
                _infoText = value;
                OnPropertyChanged("InfoText");
            }
        }
        private string _infoText;

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

        [XmlArray(ElementName = "Utilities")]
        [XmlArrayItem("Utility", Type = typeof(Utility))]
        public ObservableCollection<Utility> Utilities
        {
            get
            {
                return _utilities;
            }
            set
            {
                _utilities = value;
                OnPropertyChanged("Utilities");
            }
        }
        private ObservableCollection<Utility> _utilities;

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

        public override string ToString()
        {
            return $"Recipe: {Name}, Number: {Number}, Description: {Description}, CategoryID: {CategoryID}";
        }

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

            hashTag.ID = RecipeContainer.Instance.GetNewHashTagID();

            HashTags.Remove(hashTag);

            OnPropertyChanged("HashTags");
        }

        public void AddUtility(Utility utility)
        {
            if (Utilities == null)
            {
                Utilities = new ObservableCollection<Utility>();
            }

            utility.ID = RecipeContainer.Instance.GetNewUtilityID();

            Utilities.Add(utility);

            OnPropertyChanged("Utilities");
        }

        public void RemoveUtility(Utility utility)
        {
            if (Utilities == null)
            {
                Utilities = new ObservableCollection<Utility>();
            }

            Utilities.Remove(utility);

            OnPropertyChanged("Utilities");
        }

        public bool Match(List<string> searchValues)
        {
            var retVal = false;

            if (searchValues != null && searchValues.Count > 0)
            {
                foreach (var val in searchValues.Select(x => x.ToLower()))
                {
                    if (retVal == false 
                        && (Name.ToLower().Contains(val) 
                            || Variants.Any(x => x.Preparation.ToLower().Contains(val)) 
                            || Variants.Any(x => x.Name.ToLower().Contains(val))))
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
