using System.Collections.ObjectModel;
using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;
using TrueDiplom.Views;

namespace TrueDiplom.ViewModels
{
    public class UserOrdersVM : ViewModelBase
    {
        string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<Order> userOrders;
        public ObservableCollection<Order> UserOrders
        {
            get => userOrders;
            set
            {
                userOrders = value;
                OnPropertyChanged();
            }
        }
        Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }
        RelayCommand _showCheckOrderViewCommand;
        public RelayCommand ShowCheckOrderViewCommand
        {
            get
            {
                return _showCheckOrderViewCommand ?? new RelayCommand(obj =>
                {
                    CheckOrderView checkOrderView = new CheckOrderView();
                    CheckOrderVM checkOrderVM = new CheckOrderVM();
                    checkOrderVM.OrderId = SelectedOrder.Id;
                    checkOrderVM.OrderProduct = DbControl.GetAllProductsInOrder(checkOrderVM.OrderId);
                    checkOrderVM.OrderName = SelectedOrder.Name;
                    checkOrderVM.UserName = UserName;
                    checkOrderVM.SumAllProducts = 0;
                    foreach (var item in checkOrderVM.OrderProduct)
                    {
                        checkOrderVM.SumAllProducts += (item.ProductEntity.Price * item.Count);
                    }
                    Window win = new Window
                    {
                        Content = checkOrderView,
                        DataContext = checkOrderVM,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Height = 450,
                        Width = 800,
                        Title = "Описание заказа"
                    };
                    win.Show();
                });
            }
        }
    }
}
