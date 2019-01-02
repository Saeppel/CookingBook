using CookingLib.Container;
using CookingLib.Helper;
using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        #region Constructor

        public CategoryViewModel()
        {
            var categories = CategoryContainer.Instance.Categories;

            var cloned = new List<Category>();

            foreach (var category in categories)
            {
                cloned.Add(new Category()
                {
                    ID = category.ID,
                    Name = category.Name
                });
            }

            ClonedCategories = new ObservableCollection<Category>(cloned);
        }

        #endregion

        #region Properties

        public ObservableCollection<Category> ClonedCategories
        {
            get
            {
                return _clonedCategories;
            }
            set
            {
                _clonedCategories = value;
                OnPropertyChanged("ClonedCategories");
            }
        }
        private ObservableCollection<Category> _clonedCategories;

        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        private Category _selectedCategory;

        #endregion

        #region Methods

        public void AddCategory(Category category)
        {
            ClonedCategories.Add(category);

            OnPropertyChanged("ClonedCategories");
        }

        public void SaveCategories()
        {
            try
            {
                if (ClonedCategories != null)
                {
                    foreach (var category in ClonedCategories)
                    {
                        CategoryContainer.Instance.UpdateEntity(category);
                    }

                    if (FTPHelper.HasInstance)
                    {
                        CategoryContainer.Instance.UploadData();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
    }
}
