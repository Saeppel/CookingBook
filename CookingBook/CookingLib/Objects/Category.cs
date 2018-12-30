using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Objects
{
    public class Category : ObjectBase
    {
        public Category()
        {
        }

        public Category(long id, string text)
        {
            ID = id;
            Text = text;
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private string _text;
    }
}
