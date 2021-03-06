﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    [XmlRootAttribute("HashTag", IsNullable = false)]
    public class HashTag : ObjectBase
    {
        public HashTag()
        {
            Text = "#";
        }

        #region Properties

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

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"HashTag: {Text}";
        }

        #endregion
    }
}
