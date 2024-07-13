using System.IO;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class UserProfileVM : ViewModelBase
    {

        #region CONSTANTS.NULL_IMG

        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        #endregion

        #region VARIABLES

            #region User

        User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        #endregion

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

            #region Name
        string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region Phone
        string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region ImageSource

        BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        public delegate void EditUser(User user);
        public event EditUser EditUserHandler;
        public delegate void Exit();
        public event Exit ExitHandler;
        private RelayCommand _showEditUserCommand;
        public RelayCommand ShowEditUserCommand
        {
            get
            {
                return _showEditUserCommand ?? new RelayCommand(obj =>
                {
                    EditUserHandler.Invoke(User);
                });
            }
        }
        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return _showEditUserCommand ?? new RelayCommand(obj =>
                {
                    ExitHandler.Invoke();
                });
            }
        }
    }
}
