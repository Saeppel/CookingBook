using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("HashTag", Namespace = "", IsNullable = false)]
    public class HashTag : ObjectBase
    {
        public HashTag()
        {
            Text = "#";
        }

        [XmlElement("Text")]
        public string Text
        {
            get
            {
                return _text ?? string.Empty;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Name");
            }
        }
        private string _text;
    }
}
