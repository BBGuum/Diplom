using System.Windows;
using System.Windows.Controls;
using TrueDiplom.ViewModels;

namespace TrueDiplom.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as LoginVM).Password = PasswordBox.Password;
        }
    }
}
