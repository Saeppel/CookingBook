using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.ReportData
{
    public class ReportData
    {
        #region Constructor

        public ReportData(Recipe recipe)
        {
            Recipe = recipe;
        }

        #endregion

        #region Properties

        public Recipe Recipe
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        #endregion
    }
}
