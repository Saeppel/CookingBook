using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Objects
{
    public class OvenSetting : ObjectBase
    {
        #region Constructor

        public OvenSetting(long id, string name)
        {
            ID = id;
            Name = name;
        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _name;

        #endregion
    }
}
