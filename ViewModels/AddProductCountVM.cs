using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrueDiplom.Models;
using TrueDiplom.Utilities;

namespace TrueDiplom.ViewModels
{
    public class AddProductCountVM : ViewModelBase
    {
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
        public delegate void Close();
        public event Close OnClose;
        public delegate void Refresh(int userId);
        public event Close OnRefresh;
        RelayCommand _addCountCommand;
        public RelayCommand AddCountCommand
        {
            get
            {
                return _addCountCommand ?? new RelayCommand(ogj =>
                {
                    if (Count > 0)
                    {
                        Product product = DbControl.GetProduct(ProductId);
                        product.Count += Count;
                        DbControl.UpdateProduct(product);
                        OnRefresh.Invoke();
                        OnClose.Invoke();
                    }
                    else
                    {
                        MessageBox.Show("Количество должно быть натуральным числом");
                    }

                });
            }
        }
    }
}
