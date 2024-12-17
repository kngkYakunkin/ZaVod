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
using System.Windows.Shapes;
using ZaVod.Services;
using ZaVod.ViewModels;

namespace ZaVod
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        private static Auth _instance;
        public Auth()
        {
            InitializeComponent();
            _instance = this;
        }
        public static Auth Instance => _instance;
        public void OnCloseRequested()
        {
            this.Close();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (passwordBox != null)
            {
                AuthViewModel viewModel = (AuthViewModel)DataContext;

                if (viewModel != null)
                {
                    // Устанавливаем пароль в модель
                    viewModel.authModel.Password = passwordBox.Password;
                }
            }
        }
    }
}
