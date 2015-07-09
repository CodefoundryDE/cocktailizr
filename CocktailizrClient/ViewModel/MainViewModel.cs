using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.Message;
using GalaSoft.MvvmLight.CommandWpf;

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            using (var client = new AdminServiceClient())
            {
                client.ClientCredentials.UserName.UserName = "Admin";
                client.ClientCredentials.UserName.Password = "Cocktailizor";
            }

            InitializeMenuCommands();
        }

        #endregion

        #region Methods

        private void InitializeMenuCommands()
        {
            MenuCommands = new ObservableCollection<KeyValuePair<object, object>>()
            {
                new KeyValuePair<object, object>("Login", new RelayCommand(CallLoginDialog)),                
                new KeyValuePair<object, object>("Über Cocktailizr", new RelayCommand(CallLoginDialog)),
                new KeyValuePair<object, object>("Beenden", new RelayCommand(CallLoginDialog)),
            };
        }

        private void CallLoginDialog()
        {
            MessengerInstance.Send(new LoginMessage() { LoginAction = LoginAction.Show });
        }


        #endregion
    }
}