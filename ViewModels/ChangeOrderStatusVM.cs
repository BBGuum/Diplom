using System.Collections.ObjectModel;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class ChangeOrderStatusVM : ViewModelBase
    {
        ObservableCollection<OrderStatus> orderStatuses;
        public ObservableCollection<OrderStatus> OrderStatuses
        {
            get => orderStatuses;
            set
            {
                orderStatuses = value;
                OnPropertyChanged();
            }
        }
        OrderStatus selectedStatus;
        public OrderStatus SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }
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
        Order order;
        public Order Order
        {
            get => order;
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }
        public delegate void Close();
        public event Close OnClose;
        public delegate void Orders();
        public event Orders OrdersHandler;
        RelayCommand _editStatusCommand;
        public RelayCommand EditStatusCommand
        {
            get
            {
                return _editStatusCommand ?? new RelayCommand(obj =>
                {
                    Order editedOrder = Order;
                    editedOrder.StatusId = SelectedValue;
                    DbControl.UpdateOrder(editedOrder);
                    OnClose.Invoke();
                    OrdersHandler.Invoke();
                });
            }
        }
    }
}
