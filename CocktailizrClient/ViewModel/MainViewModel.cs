using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using CocktailizrTypes.Security;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;

namespace CocktailizrClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : CocktailizrClientViewModelBase
    {
        #region Properties

        private ObservableCollection<KeyValuePair<object, object>> _menuCommands;
        public ObservableCollection<KeyValuePair<object, object>> MenuCommands
        {
            get { return _menuCommands; }
            set
            {
                _menuCommands = value;
                RaisePropertyChanged(() => MenuCommands);
            }
        }


        private bool _impressumVisible;
        public bool ImpressumVisible
        {
            get { return _impressumVisible; }
            set
            {
                _impressumVisible = value;
                RaisePropertyChanged(() => ImpressumVisible);
            }
        }

        #endregion

        #region Variables

        private ViewModelLocator _viewModelLocator;

        private RelayCommand _loginCommand;
        private KeyValuePair<object, object> _loginKvp;
        private RelayCommand _logoutCommand;
        private KeyValuePair<object, object> _logoutKvp;
        private RelayCommand _ueberCocktailizrCommand;
        private KeyValuePair<object, object> _ueberCocktailizrKvp;
        private RelayCommand _beendenCommand;
        private KeyValuePair<object, object> _beendenKvp;
        private RelayCommand _neuerCocktailCommand;
        private KeyValuePair<object, object> _neuerCoktailKvp;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ViewModelLocator viewModelLocator)
        {
            _viewModelLocator = viewModelLocator;

            MessengerInstance.Register<LoginMessage>(this, RecieveLoginMessage);

            InitializeMenuCommands();
        }

        #endregion

        #region Methods

        private void InitializeMenuCommands()
        {
            _loginCommand = new RelayCommand(NavigateToLogin);
            _loginKvp = new KeyValuePair<object, object>("Login", _loginCommand);

            _logoutCommand = new RelayCommand(SendLogOutRequest);
            _logoutKvp = new KeyValuePair<object, object>("Logout", _logoutCommand);

            _beendenCommand = new RelayCommand(ShutApplicationDown);
            _beendenKvp = new KeyValuePair<object, object>("Beenden", _beendenCommand);

            _neuerCocktailCommand = new RelayCommand(NavigateToCocktailEdit);
            _neuerCoktailKvp = new KeyValuePair<object, object>("Neuer Cocktail", _neuerCocktailCommand);

            _ueberCocktailizrCommand = new RelayCommand(NavigateToImpressum);
            _ueberCocktailizrKvp = new KeyValuePair<object, object>("Über Cocktailizr", _ueberCocktailizrCommand);

            MenuCommands = new ObservableCollection<KeyValuePair<object, object>>()
            {
                _loginKvp,
                _ueberCocktailizrKvp,
                _beendenKvp
            };
        }

        private void NavigateToLogin()
        {
            MessengerInstance.Send(new LoginMessage() { LoginAction = LoginAction.Show });
        }

        private void SendLogOutRequest()
        {
            MessengerInstance.Send(new LoginMessage() { LoginAction = LoginAction.Logout });
        }

        private void ShutApplicationDown()
        {
            Application.Current.Shutdown();
        }

        private void NavigateToCocktailEdit()
        {
            _viewModelLocator.Search.IsVisible = false;
            _viewModelLocator.Cocktail.IsVisible = false;
            _viewModelLocator.ExtendedSearch.IsVisible = false;
            MessengerInstance.Send(new LoadAdminMessage { CocktailToBeEdited = new Cocktail() });
        }

        private void NavigateToImpressum()
        {
            ImpressumVisible = true;
        }

        private void RecieveLoginMessage(LoginMessage message)
        {
            if (LoginAction.RoleChange != message.LoginAction)
            {
                return;
            }
            if (_viewModelLocator.AdminServiceClient.GetUserRole() == UserRole.Admin)
            {
                //Einloggen erfolte gerade als Admin
                //Entfernen des Login-Buttons, einhängen eines Logout-Buttons
                DispatcherHelper.CheckBeginInvokeOnUI(() => MenuCommands.Remove(_loginKvp));
                DispatcherHelper.CheckBeginInvokeOnUI(() => MenuCommands.Insert(0, _logoutKvp));
                DispatcherHelper.CheckBeginInvokeOnUI(() => MenuCommands.Insert(1, _neuerCoktailKvp));
            }
            else
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => MenuCommands.Remove(_logoutKvp));
                DispatcherHelper.CheckBeginInvokeOnUI(() => MenuCommands.Remove(_neuerCoktailKvp));
                DispatcherHelper.CheckBeginInvokeOnUI(() => MenuCommands.Insert(0, _loginKvp));
            }

        }

        #endregion
    }
}