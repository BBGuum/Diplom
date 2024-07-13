using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class CatologueVM : ViewModelBase
    {

        #region CONSTANTS

        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        #endregion

        #region VARIABLES
            #region User
        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
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
        int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;
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

        BitmapImage _bitImage;
        public BitmapImage BitImage
        {
            get => _bitImage;
            set
            {
                _bitImage = value;
                /*if (!string.IsNullOrEmpty(ImageSource))
                {
                    BitImage = new BitmapImage();
                    using (Stream stream = File.OpenRead(ImageSource))
                    {
                        BitImage.BeginInit();
                        BitImage.CacheOption = BitmapCacheOption.OnLoad;
                        BitImage.StreamSource = stream;
                        BitImage.EndInit();
                    }
                }
                OnPropertyChanged();*/
            }
        }

        #endregion

            #region Products

        ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region SelectedProductItem

        Product _selectedProductItem;
        public Product SelectedProductItem
        {
            get => _selectedProductItem;
            set
            {
                _selectedProductItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region EVENTHANDLERS

        #region ShowProductHandler
        public delegate void ShowProduct(Product product);
        public event ShowProduct ShowProductHandler;
        #endregion

        #region EditProductHandler
        public delegate void EditProduct(Product product);
        public event EditProduct EditProductHandler; 
        #endregion

        #endregion

        #region COMMANDS

            #region ShowProductViewCommand

        RelayCommand _showProductViewCommand;
        public RelayCommand ShowProductViewCommand
        {
            get
            {
                return _showProductViewCommand ?? new RelayCommand(obj =>
                {
                    GetProductItem(UserId);
                });
            }
        }

        #endregion

            #region ShowEditProductViewCommand

        RelayCommand _showEditProductViewCommand;
        public RelayCommand ShowEditProductViewCommand
        {
            get
            {
                return _showProductViewCommand ?? new RelayCommand(obj =>
                {
                    EditProductItem();
                });
            }
        }

        #endregion

        #endregion

        #region METHODS

        /*private void DeleteProductItem(Product product)
        {
            if (product != null)
            {
                if (product.ImageSource != null)
                {
                    File.Delete(product.ImageSource);
                }
                DbControl.DeleteProduct(product);
            }
        }*/
        private void GetProductItem(int userId)
        {
            if (SelectedProductItem != null)
            {
                Product? product = DbControl.IsProductExist(SelectedProductItem.Name);
                ShowProductHandler.Invoke(product);
            }
            else
            {
                MessageBox.Show("Товара не существует");
            }
        }
        private void EditProductItem()
        {
            if (SelectedProductItem != null)
            {
                Product? product = DbControl.IsProductExist(SelectedProductItem.Name);
                EditProductHandler.Invoke(product);
            }
            else
            {
                MessageBox.Show("Товара не существует");
            }
        } 

        #endregion

        Visibility _editProductViewVisibility = Visibility.Collapsed;
        public Visibility EditProductViewVisibility
        {
            get => _editProductViewVisibility;
            set
            {
                _editProductViewVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}
