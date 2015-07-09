using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrTypes.Model.Entities;

namespace CocktailizrClient.ViewModel
{
    public class AdminViewModel : CocktailizrClientViewModelBase
    {
        #region Proprties
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



        public AdminServiceClient AdminServiceClient { get; set; }

        public CocktailServiceClient CocktailServiceClient { get; set; }
        #endregion

        #region Commands

        #endregion

        #region Constructor
        public AdminViewModel(AdminServiceClient adminServiceClient, CocktailServiceClient cocktailServiceClient)
        {
            AdminServiceClient = adminServiceClient;
            CocktailServiceClient = cocktailServiceClient;
        }
        #endregion

        #region Methods

        #endregion

    }
}