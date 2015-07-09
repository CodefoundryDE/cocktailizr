using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;

namespace CocktailizrClient.ViewModel
{
    public class LoginViewModel : CocktailizrClientViewModelBase
    {

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public ICommand ButtonLoginCommand { get { return new RelayCommand(ButtonLoginClick); } }
        public ICommand ButtonCancelCommand { get { return new RelayCommand(ButtonCancelClick); } }

        public LoginViewModel()
        {
            // Debug
            IsVisible = true;
        }

        private void ButtonLoginClick()
        {
            MessageBox.Show(Password);
        }

        private void ButtonCancelClick()
        {
            IsVisible = false;
        }


    }
}
