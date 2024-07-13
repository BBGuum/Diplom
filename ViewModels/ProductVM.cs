using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;
using TrueDiplom.Views;

namespace TrueDiplom.ViewModels
{
    public class ProductVM : ViewModelBase
    {

        #region VARIABLES

            #region User

        User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                if (User.RoleId == 2)
                {
                    AddCountProductVisibility = Visibility.Visible;
                }
                OnPropertyChanged();
            }
        }

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

            #region ImageSoure

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
                OnPropertyChanged();
            }
        } 

        #endregion

        #endregion

        #region EVENTHANDLERS.RefreshCartTableHandler
        public delegate void RefreshProduct(Product product);
        public event RefreshProduct RefreshProductHandler;

        #endregion

        #region COMMANDS.AddProductToCartCommand

        RelayCommand _addProductToCartCommand;
        public RelayCommand AddProductToCartCommand
        {
            get
            {
                return _addProductToCartCommand ?? new RelayCommand(obj =>
                {
                    AddProductToCart();
                });
            }
        }

        #endregion

        RelayCommand _showAddOrderCountCommand;
        public RelayCommand ShowAddOrderCountCommand
        {
            get
            {
                return _showAddOrderCountCommand ?? new RelayCommand(obj =>
                {
                    AddProductCountView addProductCountView = new AddProductCountView();
                    AddProductCountVM addProductCountVM = new AddProductCountVM();
                    addProductCountVM.ProductId = ProductId;
                    addProductCountVM.Count = 0;
                    addProductCountVM.OnRefresh += RefreshProductView;
                    Window window = new Window
                    {
                        Content = addProductCountView,
                        DataContext = addProductCountVM,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStyle = WindowStyle.ToolWindow,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Height = 120,
                        Width = 200,
                        Title = ""
                    };
                    addProductCountVM.OnClose += window.Close;
                    window.ShowDialog();
                });
            }
        }
        #region METHODS.AddProductToCart
        public void AddProductToCart()
        {
            Cart cart = new Cart();
            cart.UserId = User.Id;
            cart.ProductId = ProductId;
            using (var db = new DbAppContext())
            {
                bool check = db.Carts.Any(p => p.UserId == cart.UserId && p.ProductId == cart.ProductId);
                if (!check)
                {
                    DbControl.AddCartProduct(new Cart
                    {
                        UserId = cart.UserId,
                        ProductId = cart.ProductId,
                        Count = 1
                    });
                }
                else
                {
                    DbControl.PlusCountOfProducts(cart);
                }

            }
        }
        #endregion
        private void RefreshProductView()
        {
            Product product = DbControl.GetProduct(ProductId);
            RefreshProductHandler.Invoke(product);
        }

        Visibility _addCountProductVisibility = Visibility.Collapsed;
        public Visibility AddCountProductVisibility
        {
            get => _addCountProductVisibility;
            set
            {
                _addCountProductVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}
