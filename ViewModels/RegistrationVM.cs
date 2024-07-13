using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class RegistrationVM : ViewModelBase
    {

        #region CONSTANT

        private const string _imageSourcePath = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\Users";

        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        #endregion

        #region VARIABLES

            #region OpenFileDialog

        private OpenFileDialog _img;

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

            #region Password1

        string _password1;
        public string Password1
        {
            get => _password1;
            set
            {
                _password1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region Password2

        string _password2;
        public string Password2
        {
            get => _password2;
            set
            {
                _password2 = value;
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

        string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region BitImage

        BitmapImage _bitImage = new BitmapImage( new Uri (NULL_IMG));
        public BitmapImage BitImage
        {
            get => _bitImage;
            set
            {
                _bitImage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region EVENTHANDLERS.LoginHandler

        public delegate void LoginView();
        public event LoginView LoginHandler;

        #endregion

        #region COMMANDS

            #region ShowLoginViewCommand

        private RelayCommand showLoginViewCommand;
        public RelayCommand ShowLoginViewCommand
        {
            get
            {
                return showLoginViewCommand ?? new RelayCommand(obj =>
                {
                    LoginHandler.Invoke();
                });
            }
        }

        #endregion

            #region UserRegistrationCommand

        private RelayCommand _userRegistrationCommand;
        public RelayCommand UserRegistrationCommand
        {
            get
            {
                return _userRegistrationCommand ?? new RelayCommand(obj =>
                {
                    bool IsRegistered = UserRegistration();
                    if (IsRegistered)
                    {
                        MessageBox.Show($"Вы успешно зарегистрировались!\nВаш логин: {Login}\nВаш пароль: {Password1}");
                        LoginHandler.Invoke();
                    }
                });
            }
        }

        #endregion

            #region AddImageSourceCommand
        private RelayCommand _addImageSourceCommand;
        public RelayCommand AddImageSourceCommand
        {
            get
            {
                return _addImageSourceCommand ?? new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Images (*.jpg, *.png)|*.jpg;*.png;*.JPG;*.PNG";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _img = openFileDialog;
                        BitImage = new BitmapImage(new Uri(openFileDialog.FileName));
                    }
                });
            }
        }
        #endregion

        #endregion

        #region METHOD.UserRegistration

        private bool UserRegistration()
        {
            if (!(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password1) || string.IsNullOrEmpty(Password2) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone)))
            {
                if (!DbControl.IsNameExist(Login))
                {
                    if (!Login.Contains(" "))
                    {
                        if (!Password1.Contains(" "))
                        {
                            if (Password1.Length >= 6)
                            {
                                if (Password1 == Password2)
                                {
                                    string filePath = _img == null ? "" : Path.Combine(_imageSourcePath, _img.SafeFileName);
                                    if (!string.IsNullOrEmpty(filePath))
                                    {
                                        File.Copy(_img.FileName, filePath, true);
                                    }
                                    DbControl.AddUser(new User
                                    {
                                        Login = Login,
                                        Password = Password1,
                                        Name = Name,
                                        Phone = Phone,
                                        ImageSource = filePath,
                                        RoleId = 1
                                    });
                                    BitImage = new BitmapImage(new Uri(NULL_IMG));
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("Пароли не совпадают");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароль должен быть больше 6 символов");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Уберите пробелы в пароле");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Уберите пробелы в логине");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Такой логин уже существует");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Не все обязательные данные введены");
                return false;
            }
        } 

        #endregion

    }
}
