using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("HashTag", IsNullable = false)]
    public class Category : ObjectBase
    {
        public Category()
        {
        }

        public Category(long id, string name)
        {
            ID = id;
            Name = name;
        }

        #region Properties

        [XmlElement("Name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _name;

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Category: {Name}";
        }

        #endregion
    }
}
