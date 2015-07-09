using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrClient.Properties;

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

        private ViewModelLocator _viewModelLocator;

        public LoginViewModel(ViewModelLocator viewModelLocator)
        {
            _viewModelLocator = viewModelLocator;

            MessengerInstance.Register<LoginMessage>(this, ReceiveLoginMessage);
        }

        private void ReceiveLoginMessage(LoginMessage message)
        {
            if (message.LoginAction == LoginAction.Show)
            {
                IsVisible = true;
            }
            if (message.LoginAction == LoginAction.Logout)
            {
                Logout();
            }
        }

        private void ButtonLoginClick()
        {
            var credentials = new ClientCredentials();
            credentials.UserName.UserName = UserName;
            credentials.UserName.Password = Password;

            bool exceptionFlag = false;

            try
            {
                _viewModelLocator.LoginServiceClients(credentials);

                if (_viewModelLocator.AdminServiceClient.CredentialsOk())
                {
                    // Bei Erfolg
                    IsVisible = false;
                    MessengerInstance.Send(new LoginMessage() { LoginAction = LoginAction.RoleChange });
                    UserName = string.Empty;
                    Password = string.Empty;
                    //MessageBox.Show(_viewModelLocator.AdminServiceClient.GetUserRole().ToString());
                }
                else
                {
                    exceptionFlag = true;
                }
            }
            catch
            {
                // Login fehlgeschlagen
                exceptionFlag = true;
            }
            if (exceptionFlag)
            {
                MessageBox.Show("Username oder Passwort sind inkorrekt!");
                Password = string.Empty;
            }

        }

        private void Logout()
        {
            //Logout entspricht einem Login als "ANONYMOUS"
            var credentials = new ClientCredentials();
            credentials.UserName.UserName = Resources.AnonymousCredentials;
            credentials.UserName.Password = Resources.AnonymousCredentials;

            bool exceptionFlag = false;

            try
            {
                _viewModelLocator.LoginServiceClients(credentials);
                MessengerInstance.Send(new LoginMessage() { LoginAction = LoginAction.RoleChange });

            }
            catch
            {
                // Login fehlgeschlagen
                exceptionFlag = true;
            }
            if (exceptionFlag)
            {
                MessageBox.Show("Fehler beim Logout!");
            }

        }

        private void ButtonCancelClick()
        {
            IsVisible = false;
            MessengerInstance.Send(new LoginMessage() { LoginAction = LoginAction.Cancel });
        }


    }
}
