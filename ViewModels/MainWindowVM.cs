using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {

        #region VARIABLES

        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region VIEWMODELS

        LoginVM _loginVM;
        RegistrationVM _registrationVM;
        HomeVM _homeVM;

        #endregion

        #region COMMANDS

            #region OpenLoginViewCommand

        private RelayCommand _openLoginViewCommand;
        public RelayCommand OpenLoginViewCommand
        {
            get
            {
                return _openLoginViewCommand ?? new RelayCommand(obj =>
                {
                    ShowLoginView();
                });
            }
        }

        #endregion

            #region OpenRegisterViewCommand

        private RelayCommand _openRegisterViewCommand;
        public RelayCommand OpenRegisterViewCommand
        {
            get
            {
                return _openRegisterViewCommand ?? new RelayCommand(obj =>
                {
                    ShowRegisterView();
                });
            }
        }

        #endregion

        #endregion

        #region CONSTRUCTOR
        public MainWindowVM()
        {
            ShowLoginView();
        }
        #endregion

        #region METHODS

        private void ShowHomeView(User user)
        {
            _homeVM = new HomeVM();
            if(user.RoleId == 2)
            {
                _homeVM.UsersViewVisibility = Visibility.Visible;
                _homeVM.AddProductViewVisibility = Visibility.Visible;
                _homeVM.OrdersViewVisibility = Visibility.Visible;
            }
            _homeVM.User = user;
            _homeVM.ExitHandler += ShowLoginView;
            CurrentView = _homeVM;
        }
        private void ShowLoginView()
        {
            _loginVM = new LoginVM();
            _loginVM.Register += ShowRegisterView;
            _loginVM.HomeVMHandler += ShowHomeView;
            CurrentView = _loginVM;
        }
        private void ShowRegisterView()
        {
            _registrationVM = new RegistrationVM();
            _registrationVM.Phone = "+7";
            _registrationVM.LoginHandler += ShowLoginView;
            CurrentView = _registrationVM;
        } 

        #endregion

    }
}
