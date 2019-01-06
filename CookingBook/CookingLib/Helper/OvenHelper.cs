using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Helper
{
    public class OvenHelper
    {
        #region Constructor

        private OvenHelper()
        {
            _ovenSettings = new List<OvenSetting>()
            {
                new OvenSetting(0, string.Empty),
                new OvenSetting(1, "Ober-/Unterhitze"),
                new OvenSetting(2, "Oberhitze"),
                new OvenSetting(3, "Unterhitze"),
                new OvenSetting(4, "Umluft"),
                new OvenSetting(4, "Heißluft"),
                new OvenSetting(4, "Grill"),
                new OvenSetting(4, "Umluftgrill"),
            };
        }


        #endregion

        #region Singleton

        public static OvenHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OvenHelper();
                }

                return _instance;
            }
        }
        private static OvenHelper _instance;

        #endregion

        #region Properties

        public List<OvenSetting> OvenSettings
        {
            get
            {
                return _ovenSettings;
            }
        }
        private List<OvenSetting> _ovenSettings;


        #endregion
    }
}
