using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class Cart : ViewModelBase
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(count));
            }
        }
        public Product ProductEntity { get; set; }
        public User UserEntity { get; set; }
    }
}
