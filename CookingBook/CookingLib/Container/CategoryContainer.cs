using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Container
{
    public class CategoryContainer : ContainerBase<Category>
    {
        #region Fields

        private static CategoryContainer _instance;

        #endregion

        #region Constructor

        private CategoryContainer()
        {
            LoadAllEntities();
        }

        #endregion

        #region Properties

        public static CategoryContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CategoryContainer();
                }

                return _instance;
            }
        }

        public List<Category> Categories
        {
            get
            {
                return Entities;
            }
        }

        #endregion
    }
}
