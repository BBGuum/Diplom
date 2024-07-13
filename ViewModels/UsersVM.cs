using System.Collections.ObjectModel;
using System.IO;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class UsersVM : ViewModelBase
    {

        #region VARIABLES
        int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

            #region Users
        ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        #endregion

            #region SelectedUser

        User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        public delegate void EditUser(User user);
        public event EditUser EditUserHandler;
        public delegate void DeleteUser();
        public event DeleteUser DeleteUserHandler;
        RelayCommand editUserCommand;
        public RelayCommand EditUserCommand
        {
            get
            {
                return editUserCommand ?? new RelayCommand(obj =>
                {
                    EditUserHandler.Invoke(SelectedUser);
                });
            }
        }
        RelayCommand deleteUserCommand;
        public RelayCommand DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ?? new RelayCommand(obj =>
                {
                    DeleteSelectedUser(SelectedUser);
                    DeleteUserHandler.Invoke();
                });
            }
        }
        void DeleteSelectedUser(User user)
        {
            if (user.Id != UserId)
            {
                if (user.ImageSource != null)
                    File.Delete(user.ImageSource);
                DbControl.DeleteUser(user);
            }
        }
    }
}
