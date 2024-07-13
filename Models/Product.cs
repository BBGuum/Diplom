using System.Collections.ObjectModel;
using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class Product : ViewModelBase
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        int price, count;
        string name, definition, image_source;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string ImageSource
        {
            get => image_source;
            set
            {
                image_source = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public string Definition
        {
            get => definition;
            set
            {
                definition = value;
                OnPropertyChanged(nameof(Definition));
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public Category CategoryEntity { get; set; }
        public ObservableCollection<Order> OrderEntities { get; set; }
        public ObservableCollection<User> UserEntities { get; set; }
        public ObservableCollection<Cart> CartEntities { get; set; }
        public ObservableCollection<OrderProduct> OrderProducts { get; set; }
    }
}
