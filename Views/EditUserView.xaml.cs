using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrueDiplom.ViewModels;

namespace TrueDiplom.Views
{
    /// <summary>
    /// Логика взаимодействия для EditUserView.xaml
    /// </summary>
    public partial class EditUserView : UserControl
    {
        public EditUserView()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as EditUserVM).UserPassword = userPass.Password;
        }
        private void PasswordBox_PasswordChanged1(object sender, RoutedEventArgs e)
        {
            (DataContext as EditUserVM).Password1 = pass1.Password;
        }
        private void PasswordBox_PasswordChanged2(object sender, RoutedEventArgs e)
        {
            (DataContext as EditUserVM).Password2 = pass2.Password;
        }
    }
}
