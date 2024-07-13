using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class AddProductVM : ViewModelBase
    {

        #region CONSTANTS

        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        const string _imageSourcePath = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\Products";

        #endregion

        #region VARIABLES

            #region OpenFileDialog

        private OpenFileDialog _img;

        #endregion

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

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

            #region Price

        int _price;
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region Count

        int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region Categories

        ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region SelectedCategory

        Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region Definition

        string _definition;
        public string Definition
        {
            get => _definition;
            set
            {
                _definition = value;
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

        private BitmapImage _bitImage;
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

        #region EVENTHANDLERS

        #region BackToCatologue

        public delegate void CatologueVM();
        public event CatologueVM BackToCatologue;

        #endregion

        #endregion

        #region COMMANDS

            #region AddImageSource

        private RelayCommand _addImageSource;
        public RelayCommand AddImageSource
        {
            get
            {
                return _addImageSource ?? new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Images (*.jpg, *.png)|*.jpg;*.png;*.JPG;*.PNG";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _img = openFileDialog;
                        BitImage = new BitmapImage(new Uri( openFileDialog.FileName));
                    }
                });
            }
        }

        #endregion

            #region AddProductItemCommand
        private RelayCommand _addProductItemCommand;
        public RelayCommand AddProductItemCommand
        {
            get
            {
                return _addProductItemCommand ?? new RelayCommand(obj =>
                {
                    bool IsAddedProduct = AddProductItem();
                    if (IsAddedProduct)
                    {
                        BackToCatologue.Invoke();
                    }
                });
            }
        }
        #endregion

        #endregion

        #region METHODS.AddProductItem

        public bool AddProductItem()
        {
            if (!(string.IsNullOrEmpty(Name) || Price <= 0 || SelectedCategory == null || string.IsNullOrEmpty(Definition)))
            {
                if (Count >= 0)
                {
                    string filePath = _img == null ? "" : Path.Combine(_imageSourcePath, _img.SafeFileName);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        File.Copy(_img.FileName, filePath, true);
                        DbControl.AddProduct(new Product
                        {
                            CategoryId = SelectedCategory.Id,
                            Name = Name,
                            Price = Price,
                            Definition = Definition,
                            Count = Count,
                            ImageSource = filePath
                        });
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Вы не добавили изображение товара");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели некорректное количество товара");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Вы ввели не все данные");
                return false;
            }
        } 

        #endregion

    }
}
