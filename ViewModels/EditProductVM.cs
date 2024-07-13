using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;
using static TrueDiplom.ViewModels.EditUserVM;

namespace TrueDiplom.ViewModels
{
    public class EditProductVM : ViewModelBase
    {

        #region CONSTANTS

        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        private const string _imageSourcePath = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\Products";

        #endregion

        #region VARIABLES

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                if (User.RoleId == 2)
                {

                }
                _user = value;
                OnPropertyChanged();
            }
        }

        #region OpenFileDialog
        private OpenFileDialog _img; 
        #endregion

            #region ProductId

        int _productId;
        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
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

            #region SelectedValue

        int _selectedValue;
        public int SelectedValue
        {
            get => _selectedValue;
            set
            {
                _selectedValue = value;
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

        public delegate void Catologue();
        public event Catologue BackToCatologue;

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
                        BitImage = new BitmapImage(new Uri(openFileDialog.FileName));
                    }
                });
            }
        } 

        #endregion

            #region EditProductItemCommand

        private RelayCommand _editProductItemCommand;
        public RelayCommand EditProductItemCommand
        {
            get
            {
                return _editProductItemCommand ?? new RelayCommand(obj =>
                {
                    bool IsEditedProduct = EditProductItem();
                    if (IsEditedProduct)
                    {
                        Product product = DbControl.GetProduct(ProductId);
                        BackToCatologue.Invoke();
                    }
                });
            }
        }

        #endregion

        #endregion

        #region METHODS.EditProductItem
        public bool EditProductItem()
        {
            if (!(string.IsNullOrEmpty(Name) || Price <= 0 || SelectedCategory == null || string.IsNullOrEmpty(Definition)))
            {
                if (Count >= 0)
                {
                    string filePath = _img == null ? string.Empty : Path.Combine(_imageSourcePath, _img.SafeFileName);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (File.Exists(ImageSource))
                        {
                            File.Delete(ImageSource);
                        }
                        File.Copy(_img.FileName, filePath, true);
                        Product _product = new Product
                        {
                            Id = ProductId,
                            CategoryId = SelectedCategory.Id,
                            Name = Name,
                            Price = Price,
                            Definition = Definition,
                            Count = Count,
                            ImageSource = filePath
                        };
                        DbControl.UpdateProduct(_product);
                        filePath = string.Empty;
                        return true;
                    }
                    else
                    {
                        Product _product = new Product
                        {
                            Id = ProductId,
                            CategoryId = SelectedCategory.Id,
                            Name = Name,
                            Price = Price,
                            Definition = Definition,
                            Count = Count
                        };
                        DbControl.UpdateProduct(_product);
                        return true;
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
