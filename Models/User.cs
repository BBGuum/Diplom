using System.Collections.ObjectModel;
using TrueDiplom.Utilities;

namespace TrueDiplom.Models
{
    public class User : ViewModelBase
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        string login, password, name, phone;
        string? image_source;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string? ImageSource
        {
            get => image_source;
            set
            {
                image_source = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public Role RoleEntity { get; set; }
        public ObservableCollection<Product> ProductEntities { get; set; }
        public ObservableCollection<Cart> CartEntities { get; set; }
        public ObservableCollection<Order> OrderEntities { get; set; }
    }
}
