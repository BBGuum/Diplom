using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class LoginVM : ViewModelBase
    {

        #region VARIABLES

            #region Login

        string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region Password

        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region EVENTHANDLERS

        #region Register
        public delegate void RegisterHandler();
        public event RegisterHandler Register;
        #endregion

        #region HomeVMHandler
        public delegate void HomeVM(User user);
        public event HomeVM HomeVMHandler; 
        #endregion

        #endregion

        #region COMMANDS

        #region ShowRegisterViewCommand

        private RelayCommand showRegisterViewCommand;
        public RelayCommand ShowRegisterViewCommand
        {
            get
            {
                return showRegisterViewCommand ?? new RelayCommand(obj =>
                {
                    Register.Invoke();
                });
            }
        }

        #endregion

            #region LoginCommand

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? new RelayCommand(obj =>
                {
                    Auth();
                });
            }
        }

        #endregion

        #endregion

        #region METHODS.Auth

        private void Auth()
        {
            User? user = DbControl.IsAuthorized(Login, Password);
            if (user != null)
            {
                HomeVMHandler.Invoke(user);
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        } 

        #endregion

    }
}
