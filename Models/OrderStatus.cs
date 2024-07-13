using System.Collections.ObjectModel;
using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class OrderStatus : ViewModelBase
    {
        public int Id { get; set; }
        string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public ObservableCollection<Order> OrderEntites { get; set; }
    }
}
