using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZaVod.Models;
using ZaVod.Services;
using ZaVod.Views;
using ZaVod.Repositories;
using Menu = ZaVod.Views.Menu;


namespace ZaVod.ViewModels
{
    internal class AuthViewModel
    {
        public AuthModel authModel { get; set; }

        private bool _errorVisible;

        public bool ErrorVisible
        {
            get { return _errorVisible; }
            set
            {
                if (_errorVisible != value) 
                {
                    _errorVisible = value;
                    OnPropertyChanged(nameof(ErrorVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand AuthCommand{ get; set; }

        private readonly AuthService authService;


        public AuthViewModel()
        {
            this.authModel = new AuthModel();
            this.AuthCommand = new RelayCommand(ExecuteLogin);
            this.authService = new AuthService();
            ErrorVisible = false;
        }

        private void ExecuteLogin(object parameter)
        {
            string login = authModel.Login;
            string password = authModel.Password;
            
            if (login != null && password != null && this.authService.ValidateUser(login, password))
            {
                WorkerRepository workerRepository = new WorkerRepository();
                Worker worker = workerRepository.GetWorker(login);
                Menu main = new Menu(worker);
                main.Show();
                Auth.Instance.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
                Console.WriteLine(ErrorVisible.ToString());
            }
        }
    }
}
