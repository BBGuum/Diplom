using System.Collections.ObjectModel;
using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class CheckOrderVM : ViewModelBase
    {
        ObservableCollection<OrderProduct> _orderProduct;
        public ObservableCollection<OrderProduct> OrderProduct
        {
            get => _orderProduct;
            set
            {
                _orderProduct = value;
                OnPropertyChanged();
            }
        }
        int orderId;
        public int OrderId
        {
            get => orderId;
            set
            {
                orderId = value;
                OnPropertyChanged();
            }
        }
        string orderName;
        public string OrderName
        {
            get => orderName;
            set
            {
                orderName = value;
                OnPropertyChanged();
            }
        }
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
        int sumAllProducts;
        public int SumAllProducts
        {
            get => sumAllProducts;
            set
            {
                sumAllProducts = value;
                OnPropertyChanged();
            }
        }

    }
}
