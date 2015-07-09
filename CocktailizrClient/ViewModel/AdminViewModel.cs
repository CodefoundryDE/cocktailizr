using System.Windows.Input;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using GalaSoft.MvvmLight.CommandWpf;

namespace CocktailizrClient.ViewModel
{
    public class AdminViewModel : CocktailizrClientViewModelBase
    {
        #region Properties

        public ViewModelLocator ViewModelLocator { get; set; }

        private Cocktail _recentCocktail;

        public Cocktail RecentCocktail
        {
            get { return _recentCocktail; }
            set
            {
                _recentCocktail = value;
                RaisePropertyChanged(() => RecentCocktail);
            }
        }
        #endregion

        #region Commands
        public ICommand SaveClickCommand
        {
            get { return new RelayCommand(SaveRecentCocktail); }
        }


        #endregion

        #region Constructor
        public AdminViewModel(ViewModelLocator viewModelLocator)
        {
            ViewModelLocator = viewModelLocator;
            MessengerInstance.Register<LoadAdminMessage>(this, ReceiveLoadAdmin);
        }
        #endregion

        #region Methods
        private void ReceiveLoadAdmin(LoadAdminMessage message)
        {
            IsVisible = true;
            if (message.CocktailToBeEdited != null)
            {
                RecentCocktail = message.CocktailToBeEdited;
            }
        }

        private void SaveRecentCocktail()
        {
            ViewModelLocator.AdminServiceClient.ModifyCocktail(RecentCocktail.Id, RecentCocktail);
        }


        #endregion

    }
}