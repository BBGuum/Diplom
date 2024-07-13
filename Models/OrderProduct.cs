using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class OrderProduct : ViewModelBase
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public Product ProductEntity { get; set; }
        public Order OrderEntity { get; set; }
    }
}
