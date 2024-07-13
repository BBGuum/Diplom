using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class HomeVM : ViewModelBase
    {

        #region CONSTANTS

        const string NULL_IMG = "C:\\Users\\boris\\source\\repos\\TrueDiplom\\TrueDiplom\\img\\null.png";

        #endregion

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
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                if(User.RoleId == 2)
                {
                    _catologue.EditProductViewVisibility = Visibility.Visible;
                }
                OnPropertyChanged();

            }
        }

        #endregion

        #region VIEWMODELS

        CatologueVM _catologue;
        UserProfileVM _userProfile;
        UsersVM _users;
        AddProductVM _addProduct;
        OrdersVM _orders;
        UserOrdersVM _userOrders;
        CartVM _cart;
        ProductVM _product;
        EditProductVM _editProduct;
        EditUserVM _editUser;

        #endregion

        public delegate void Exit();
        public event Exit ExitHandler;

        #region COMMANDS

            #region ShowCatalogueViewCommand

        RelayCommand _showCatalogueViewCommand;
        public RelayCommand ShowCatalogueViewCommand
        {
            get
            {
                return _showCatalogueViewCommand ?? new RelayCommand(obj =>
                {
                    ShowCatalogueView();
                    if (User.RoleId == 2)
                        _catologue.EditProductViewVisibility = Visibility.Visible;
                });
            }
        }

        #endregion

            #region ShowUserProfileViewCommand

        RelayCommand _showUserProfileViewCommand;
        public RelayCommand ShowUserProfileViewCommand
        {
            get
            {
                return _showUserProfileViewCommand ?? new RelayCommand(obj =>
                {
                    ShowUserProfileView(User);
                });
            }
        }

        #endregion

            #region ShowUsersViewCommand

        RelayCommand _showUsersViewCommand;
        public RelayCommand ShowUsersViewCommand
        {
            get
            {
                return _showUsersViewCommand ?? new RelayCommand(obj =>
                {
                    ShowUsersView();
                });
            }
        }

        #endregion

            #region ShowAddProductViewCommand

        RelayCommand _showAddProductViewCommand;
        public RelayCommand ShowAddProductViewCommand
        {
            get
            {
                return _showAddProductViewCommand ?? new RelayCommand(obj =>
                {
                    ShowAddProductView();
                });
            }
        }

        #endregion

            #region ShowOrdersViewCommand

        RelayCommand _showOrdersViewCommand;
        public RelayCommand ShowOrdersViewCommand
        {
            get
            {
                return _showOrdersViewCommand ?? new RelayCommand(obj =>
                {
                    ShowOrdersView();
                });
            }
        }

        #endregion

            #region ShowUserOrdersViewCommand

        RelayCommand _showUserOrdersViewCommand;
        public RelayCommand ShowUserOrdersViewCommand
        {
            get
            {
                return _showUserOrdersViewCommand ?? new RelayCommand(obj =>
                {
                    ShowUserOrdersView(User);
                });
            }
        }

        #endregion

            #region ShowCartViewCommand

        RelayCommand _showCartViewCommand;
        public RelayCommand ShowCartViewCommand
        {
            get
            {
                return _showCartViewCommand ?? new RelayCommand(obj =>
                {
                    ShowCartView(User);
                });
            }
        }

        #endregion

        #endregion

        #region CONSTRUCTOR

        public HomeVM()
        {
            ShowCatalogueView();
        }

        #endregion

        #region METHODS
        private void BackToCatologue()
        {
            _catologue.Products.Clear();
            _catologue.Products = DbControl.GetProducts();
            CurrentView = _catologue;
        }
        private void ShowAddProductView()
        {
            _addProduct = new AddProductVM();
            _addProduct.BackToCatologue += BackToCatologue;
            _addProduct.Name = string.Empty;
            _addProduct.SelectedCategory = null;
            _addProduct.Price = 0;
            _addProduct.Count = 0;
            _addProduct.Definition = string.Empty;
            _addProduct.BitImage = new BitmapImage(new Uri(NULL_IMG));
            _addProduct.Categories = DbControl.GetCategories();
            CurrentView = _addProduct;
        }
        private void ShowCatalogueView()
        {
            _catologue = new CatologueVM();
            _catologue.ShowProductHandler += ShowProductView;
            _catologue.EditProductHandler += ShowEditProductView;
            _catologue.Products = DbControl.GetProducts();
            CurrentView = _catologue;
        }
        private void ShowUserProfileView(User user)
        {
            _userProfile = new UserProfileVM();
            _userProfile.EditUserHandler += ShowEditUserView;
            _userProfile.ExitHandler += ExitToLogin;
            _userProfile.User = DbControl.GetUser(user.Id);
            _userProfile.Login = _userProfile.User.Login;
            _userProfile.Name = _userProfile.User.Name;
            _userProfile.Phone = _userProfile.User.Phone;
            if (!string.IsNullOrEmpty(_userProfile.User.ImageSource))
            {
                _userProfile.ImageSource = new BitmapImage();
                using (Stream stream = File.OpenRead(_userProfile.User.ImageSource))
                {
                    _userProfile.ImageSource.BeginInit();
                    _userProfile.ImageSource.CacheOption = BitmapCacheOption.OnLoad;
                    _userProfile.ImageSource.StreamSource = stream;
                    _userProfile.ImageSource.EndInit();
                }
            }
            else
            {
                _userProfile.ImageSource = new BitmapImage(new Uri(NULL_IMG));
            }
            CurrentView = _userProfile;
        }
        private void ShowUsersView()
        {
            _users = new UsersVM();
            _users.Users = DbControl.GetUsers();
            _users.UserId = User.Id;
            _users.EditUserHandler += ShowEditUserView;
            _users.DeleteUserHandler += ShowUsersView;
            CurrentView = _users;
        }
        private void ShowOrdersView()
        {
            _orders = new OrdersVM();
            _orders.Orders = DbControl.GetOrders();
            CurrentView = _orders;
        }
        private void ShowUserOrdersView(User user)
        {
            _userOrders = new UserOrdersVM();
            _userOrders.UserOrders = DbControl.GetUserOrders(user.Id);
            _userOrders.UserName = user.Name;
            CurrentView = _userOrders;
        }
        private void ShowCartView(User user)
        {
            _cart = new CartVM();
            _cart.UserId = user.Id;
            _cart.UserCart = DbControl.GetAllProductsInCart(_cart.UserId);
            _cart.RefreshCartHandler += RefreshCartTable;
            CurrentView = _cart;
        }
        private void ShowProductView(Product product)
        {
            _product = new ProductVM();
            _product.User = DbControl.GetUser(User.Id);
            _product.ProductId = product.Id;
            _product.Name = product.Name;
            _product.Price = product.Price;
            _product.Count = product.Count;
            _product.Definition = product.Definition;
            _product.ImageSource = product.ImageSource;
            _product.RefreshProductHandler += ShowProductView;
            if (!string.IsNullOrEmpty(_product.ImageSource))
            {
                _product.BitImage = new BitmapImage();
                using (Stream stream = File.OpenRead(_product.ImageSource))
                {
                    _product.BitImage.BeginInit();
                    _product.BitImage.CacheOption = BitmapCacheOption.OnLoad;
                    _product.BitImage.StreamSource = stream;
                    _product.BitImage.EndInit();
                }
            }
            else
            {
                _product.BitImage = new BitmapImage(new Uri(NULL_IMG));
            }
            CurrentView = _product;
        }
        private void ShowEditProductView(Product product)
        {
            _editProduct = new EditProductVM();
            _editProduct.BackToCatologue += BackToCatologue;
            _editProduct.Categories = DbControl.GetCategories();
            _editProduct.ProductId = product.Id;
            _editProduct.Name = product.Name;
            _editProduct.Price = product.Price;
            _editProduct.Count = product.Count;
            _editProduct.Definition = product.Definition;
            _editProduct.Count = product.Count;
            _editProduct.ImageSource = product.ImageSource;
            _editProduct.SelectedValue = product.CategoryId;
            if (!string.IsNullOrEmpty(_editProduct.ImageSource))
            {
                _editProduct.BitImage = new BitmapImage();
                using (Stream stream = File.OpenRead(_editProduct.ImageSource))
                {
                    _editProduct.BitImage.BeginInit();
                    _editProduct.BitImage.CacheOption = BitmapCacheOption.OnLoad;
                    _editProduct.BitImage.StreamSource = stream;
                    _editProduct.BitImage.EndInit();
                }
            }
            else
            {
                _editProduct.BitImage = new BitmapImage(new Uri(NULL_IMG));
            }
            CurrentView = _editProduct;
        }

        private void ShowEditUserView(User user)
        {
            _editUser = new EditUserVM();
            _editUser.BackToProfile += ShowUserProfileView;
            _editUser.User = user;
            _editUser.Name = user.Name;
            _editUser.Phone = user.Phone;
            if (!string.IsNullOrEmpty(_editUser.User.ImageSource))
            {
                _editUser.BitmapImage = new BitmapImage();
                using (Stream stream = File.OpenRead(_editUser.User.ImageSource))
                {
                    _editUser.BitmapImage.BeginInit();
                    _editUser.BitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    _editUser.BitmapImage.StreamSource = stream;
                    _editUser.BitmapImage.EndInit();
                }
            }
            else
            {
                _editUser.BitmapImage = new BitmapImage(new Uri(NULL_IMG));
            }
            CurrentView = _editUser;
        }
        private void RefreshCartTable(int userId)
        {
            _cart.UserCart = DbControl.GetAllProductsInCart(userId);
        }
        private void ExitToLogin()
        {
            ExitHandler?.Invoke();
        }
        #endregion
        Visibility _usersViewVisibility = Visibility.Collapsed;
        public Visibility UsersViewVisibility
        {
            get => _usersViewVisibility;
            set
            {
                _usersViewVisibility = value;
                OnPropertyChanged();
            }
        }
        Visibility _addProductViewVisibility = Visibility.Collapsed;
        public Visibility AddProductViewVisibility
        {
            get => _addProductViewVisibility;
            set
            {
                _addProductViewVisibility = value;
                OnPropertyChanged();
            }
        }
        Visibility _ordersViewVisibility = Visibility.Collapsed;
        public Visibility OrdersViewVisibility
        {
            get => _ordersViewVisibility;
            set
            {
                _ordersViewVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}
