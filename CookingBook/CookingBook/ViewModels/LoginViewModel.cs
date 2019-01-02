using CookingLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Constructor

        public LoginViewModel()
        {
            Username = ConfigHelper.Instance.Username;
            Password = ConfigHelper.Instance.Password;
        }

        #endregion

        #region Properties

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        private string _username;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        private string _password;

        public bool Remember
        {
            get
            {
                return _remember;
            }
            set
            {
                _remember = value;
                OnPropertyChanged("Remember");
            }
        }
        private bool _remember;

        #endregion

        #region Methods

        public bool CheckLogin()
        {
            var success = FTPHelper.CheckLogin(Username, Password);

            ConfigHelper.Instance.Username = Username;
            ConfigHelper.Instance.Password = Password;

            return success;
        }

        public void SaveLogin()
        {
            ConfigHelper.Instance.WriteConfig();
        }

        #endregion
    }
}
