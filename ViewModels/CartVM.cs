using System.Collections.ObjectModel;
using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;
using TrueDiplom.Views;
using TrueDiplom.ViewModels;

namespace TrueDiplom.ViewModels
{
    public class CartVM : ViewModelBase
    {

        #region VARIABLES

            #region UserId

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

            #region UserCart

        ObservableCollection<Cart> _userCart;
        public ObservableCollection<Cart> UserCart
        {
            get => _userCart;
            set
            {
                _userCart = value;
                OnPropertyChanged();
            }
        }

        #endregion

            #region SelectedCartItem

        Cart _selectedCartItem;
        public Cart SelectedCartItem
        {
            get => _selectedCartItem;
            set
            {
                _selectedCartItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region WINDOW

        public Window CheckOrderWindow;

        #endregion

        #region EVENTHANDLERS.RefreshCartHandler

        public delegate void RefreshCart(int userId);
        public event RefreshCart RefreshCartHandler;

        #endregion

        #region COMMANDS

            #region IncrementCountCommand

        RelayCommand _incrementCountCommand;
        public RelayCommand IncrementCountCommand
        {
            get
            {
                return _incrementCountCommand ?? new RelayCommand(obj =>
                {
                    DbControl.PlusCountOfProducts(SelectedCartItem);
                    RefreshCartHandler.Invoke(UserId);
                });
            }
        }

        #endregion

            #region DecrementCountCommand

        RelayCommand _decrementCountCommand;
        public RelayCommand DecrementCountCommand
        {
            get
            {
                return _decrementCountCommand ?? new RelayCommand(obj =>
                {
                    DbControl.MinusCountOfProducts(SelectedCartItem);
                    RefreshCartHandler.Invoke(UserId);
                });
            }
        }

        #endregion

            #region RemoveProductCommand

        RelayCommand _removeProductCommand;
        public RelayCommand RemoveProductCommand
        {
            get
            {
                return _removeProductCommand ?? new RelayCommand(obj =>
                {
                    DbControl.DeleteProductFromCart(SelectedCartItem);
                    RefreshCartHandler.Invoke(UserId);
                });
            }

        }

        #endregion

            #region ShowCheckOrderWindowCommand

        RelayCommand _showCheckOrderWindowCommand;
        public RelayCommand ShowCheckOrderWindowCommand
        {
            get
            {
                return _showCheckOrderWindowCommand ?? new RelayCommand(obj =>
                {
                    CreateOrder(UserCart, UserId);
                    RefreshCartHandler?.Invoke(UserId);
                });
            }

        }

        #endregion

        #endregion

        #region METHODS

        private void CreateOrder(ObservableCollection<Cart> cart, int userId)
        {
            if(UserCart.Count != 0)
            {
                foreach (var item in cart)
                {
                    Product product = DbControl.GetProduct(item.ProductId);
                    if (item.Count > product.Count)
                    {
                        MessageBox.Show($"Количество товара \"{item.ProductEntity.Name}\" в корзине превышает количество товара в наличии");
                        return;
                    }
                }
                foreach (var item in cart)
                {
                    Product product = DbControl.GetProduct(item.ProductId);
                    product.Count -= item.Count;
                    DbControl.UpdateProduct(product);
                }
                DateTime dateget = DateTime.UtcNow;
                Order order = new Order {
                    UserId = userId,
                    StatusId = 1,
                    Name = $"{userId}-{DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                    DateGet = dateget
                };
                DbControl.AddOrder(order);
                int orderId = DbControl.GetOrderId(dateget);
                for (int i = 0; i < cart.Count; i++)
                {
                    DbControl.AddOrderProduct(new OrderProduct
                    {
                        OrderId = orderId,
                        ProductId = cart[i].ProductId,
                        Count = cart[i].Count
                    });
                    DbControl.DeleteProductFromCart(cart[i]);
                }
            }
            else
            {
                MessageBox.Show("Корзина пустая");
            }
        }
        #endregion
    }
}