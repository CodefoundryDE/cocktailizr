using CocktailizrClient.Message;

namespace CocktailizrClient.ViewModel
{
    public class ExtendedSearchViewModel : CocktailizrClientViewModelBase
    {
        #region Constructor

        public ExtendedSearchViewModel()
        {
            MessengerInstance.Register<LoadSearchMessage>(this, RecieveSearchMessage);
        }

        #endregion

        private void RecieveSearchMessage(LoadSearchMessage message)
        {
            if (message.LoadExtendedSearch)
            {
                IsVisible = true;
            }
        }
    }
}