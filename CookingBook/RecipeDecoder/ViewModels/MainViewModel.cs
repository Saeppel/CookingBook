using CookingLib.Container;
using RecipeDecoder.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

                if (Directory.Exists(_path))
                {
                    var directory = new DirectoryInfo(_path);
                    Files = new ObservableCollection<FileInfo>(directory.GetFiles("*.txt"));
                }
            }
        }
        private string _path;

        public ObservableCollection<FileInfo> Files
        {
            get
            {
                return _files;
            }
            set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }
        private ObservableCollection<FileInfo> _files;
       
        #endregion

        #region Methods

        public void Read(List<FileInfo> files)
        {
            var reader = new RecipeReader();

            foreach (var file in files)
            {
                reader.Read(file.FullName);
            }

            foreach (var recipe in reader.Recipes)
            {
                RecipeContainer.Instance.UpdateEntity(recipe);
            }
        }

        #endregion
    }
}
