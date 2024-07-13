using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class EditUserVM : ViewModelBase
    {
        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        const string _imageSourcePath = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\Users";
        private OpenFileDialog _img;
        User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }
        string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }
        string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
                OnPropertyChanged();
            }
        }
        string password1;
        public string Password1
        {
            get => password1;
            set
            {
                password1 = value;
                OnPropertyChanged();
            }
        }
        string password2;
        public string Password2
        {
            get => password2;
            set
            {
                password2 = value;
                OnPropertyChanged();
            }
        }
        string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage bitmapImage;
        public BitmapImage BitmapImage
        {
            get => bitmapImage;
            set
            {
                bitmapImage = value;
                OnPropertyChanged();
            }
        }
        public delegate void Profile(User user);
        public event Profile BackToProfile;

        private RelayCommand _addImageCommand;
        public RelayCommand AddImageCommand
        {
            get
            {
                return _addImageCommand ?? new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Images (*.jpg, *.png)|*.jpg;*.png;*.JPG;*.PNG";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _img = openFileDialog;
                        BitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                    }
                });
            }
        }
        private RelayCommand _editUserCommand;
        public RelayCommand EditUserCommand
        {
            get
            {
                return _editUserCommand ?? new RelayCommand(obj =>
                {
                    if (EditUser(User))
                    {
                        BackToProfile.Invoke(User);
                    }
                });
            }
        }
        private bool EditUser(User user)
        {
            if (!(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone)))
            {
                if (string.IsNullOrEmpty(userPassword))
                {
                    string filePath = _img == null ? "" : Path.Combine(_imageSourcePath, _img.SafeFileName);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (File.Exists(ImageSource))
                        {
                            File.Delete(ImageSource);
                        }
                        File.Copy(_img.FileName, filePath, true);
                        User editedUser = user;
                        editedUser.Name = Name;
                        editedUser.Phone = Phone;
                        editedUser.ImageSource = filePath;
                        DbControl.UpdateUser(editedUser);
                        filePath = string.Empty;
                        BitmapImage = new BitmapImage(new Uri(NULL_IMG));
                        return true;
                    }
                    else
                    {
                        User editedUser = user;
                        editedUser.Name = Name;
                        editedUser.Phone = Phone;
                        DbControl.UpdateUser(editedUser);
                        return true;
                    }
                }
                else
                {
                    if(userPassword == user.Password)
                    {
                        if (!(string.IsNullOrEmpty(Password1) || string.IsNullOrEmpty(Password2)))
                        {
                            if (!(Password1.Contains(" ") || Password2.Contains(" ")))
                            {
                                if (Password1.Length >= 6)
                                {
                                    if (Password1 == Password2)
                                    {
                                        string filePath = _img == null ? "" : Path.Combine(_imageSourcePath, _img.SafeFileName);
                                        if (!string.IsNullOrEmpty(filePath))
                                        {
                                            if (File.Exists(ImageSource))
                                            {
                                                File.Delete(ImageSource);
                                            }
                                            File.Copy(_img.FileName, filePath, true);
                                            User editedUser = user;
                                            editedUser.Name = Name;
                                            editedUser.Phone = Phone;
                                            editedUser.Password = Password1;
                                            editedUser.ImageSource = filePath;
                                            DbControl.UpdateUser(editedUser);
                                            filePath = string.Empty;
                                            BitmapImage = new BitmapImage(new Uri(NULL_IMG));
                                            return true;
                                        }
                                        else
                                        {
                                            User editedUser = user;
                                            editedUser.Name = Name;
                                            editedUser.Phone = Phone;
                                            editedUser.Password = Password1;
                                            DbControl.UpdateUser(editedUser);
                                            return true;
                                        }
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
                            MessageBox.Show("Вы не ввели новый пароль");
                            return false;
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Старый пароль не является паролем пользователя");
                        return false;
                    }
                } 
            }
            else
            {
                MessageBox.Show("Не все обязательные данные введены");
                return false;
            }
        }
    }
}
