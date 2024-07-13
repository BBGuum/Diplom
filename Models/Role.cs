using System.Collections.ObjectModel;
using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class Role : ViewModelBase
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
        public ObservableCollection<User> UserEntities { get; set; }
    }
}
