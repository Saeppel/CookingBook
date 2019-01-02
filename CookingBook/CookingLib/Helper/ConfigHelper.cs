using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Helper
{
    public class ConfigHelper
    {
        #region Constructor

        private ConfigHelper()
        {
            ReadConfig();
        }

        #endregion

        #region Properties

        public string FTPServer
        {
            get
            {
                return _ftpServer;
            }
        }
        private string _ftpServer;

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public static ConfigHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigHelper();
                }

                return _instance;
            }
        }
        private static ConfigHelper _instance;

        private void ReadConfig()
        {
            _ftpServer = ConfigurationManager.AppSettings["FTPServer"];
            Username = ConfigurationManager.AppSettings["Username"];
            Password = ConfigurationManager.AppSettings["Password"];

            if (!string.IsNullOrWhiteSpace(Username))
            {
                Username = Decode(Username);
            }

            if (!string.IsNullOrWhiteSpace(Password))
            {
                Password = Decode(Password);
            }
        }

        public void WriteConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("Username");
            config.AppSettings.Settings.Add("Username", Encode(Username));

            config.AppSettings.Settings.Remove("Password");
            config.AppSettings.Settings.Add("Password", Encode(Password));

            config.Save(ConfigurationSaveMode.Modified);
        }

        #region Cryption

        public string Encode(string value)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(encbuff);
        }

        public string Decode(string value)
        {
            byte[] decbuff = Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        #endregion

        #endregion
    }
}
