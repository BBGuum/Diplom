using System.Collections.ObjectModel;
using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;
using TrueDiplom.Views;

namespace TrueDiplom.ViewModels
{
    public class OrdersVM : ViewModelBase
    {
        ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get => orders;
            set
            {
                orders = value;
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
        RelayCommand _showChangeStatusViewCommand;
        public RelayCommand ShowChangeStatusViewCommand
        {
            get
            {
                return _showChangeStatusViewCommand ?? new RelayCommand(obj =>
                {
                    ChangeOrderStatusVIew changeOrderStatusVIew = new ChangeOrderStatusVIew();
                    ChangeOrderStatusVM changeOrderStatusVM = new ChangeOrderStatusVM();
                    changeOrderStatusVM.OrderStatuses = DbControl.GetOrderStatuses();
                    changeOrderStatusVM.OrdersHandler += RefreshOrder;
                    changeOrderStatusVM.Order = SelectedOrder;
                    changeOrderStatusVM.SelectedValue = changeOrderStatusVM.Order.StatusId;
                    Window window = new Window
                    {
                        Content = changeOrderStatusVIew,
                        DataContext = changeOrderStatusVM,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStyle = WindowStyle.ToolWindow,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Height = 100,
                        Width = 250,
                        Title = "Изменение статуса"
                    };
                    changeOrderStatusVM.OnClose += window.Close;
                    window.ShowDialog();
                });
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
                    checkOrderVM.UserName = SelectedOrder.UserEntity.Name;
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
        private void RefreshOrder()
        {
            Orders = DbControl.GetOrders();
        }
    }
}
