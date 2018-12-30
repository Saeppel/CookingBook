using RecipeDecoder.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDecoder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }
        private string _path;

        public DocXReader Reader
        {
            get;
            private set;
        }
       

        #endregion

        #region Methods

        public void Read()
        {
            Reader = new DocXReader(Path);
            Reader.ReadDocument();
        }

        #endregion
    }
}
