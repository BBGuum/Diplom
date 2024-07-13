using System.Collections.ObjectModel;
using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class Order : ViewModelBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        string name;
        DateTime date_get;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public DateTime DateGet
        {
            get => date_get;
            set
            {
                date_get = value;
                OnPropertyChanged(nameof(DateGet));
            }
        }
        public User UserEntity { get; set; }
        public OrderStatus StatusEntity { get; set; }
        public ObservableCollection<Product> ProductEntities { get; set; }
        public ObservableCollection<OrderProduct> OrderProducts { get; set; }
    }
}
