using System.Collections;
using System.Collections.Generic;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;

namespace CocktailizrClient.ViewModel
{
    public class ExtendedSearchViewModel : CocktailizrClientViewModelBase
    {
        #region Properties
        private IEnumerable<Zutat> _existingIngredients;

        public IEnumerable<Zutat> ExistingIngredients
        {
            get { return _existingIngredients; }
            set { _existingIngredients = value; }
        }

        #endregion

        #region Constructor

        public ExtendedSearchViewModel(CocktailServiceClient serviceClient)
        {
            MessengerInstance.Register<LoadSearchMessage>(this, RecieveSearchMessage);
            _existingIngredients = serviceClient.GetAllZutaten();
        }

        #endregion

        #region Methods
        private void RecieveSearchMessage(LoadSearchMessage message)
        {
            if (message.LoadExtendedSearch)
            {
                IsVisible = true;
            }
        }
        #endregion
    }
}