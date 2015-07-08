﻿using System;
using System.Threading.Tasks;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;

namespace CocktailizrClient.ViewModel
{
    public class CocktailViewModel : CocktailizrClientViewModelBase
    {
        private CocktailServiceClient _serviceClient;
        private Cocktail _shownCocktail;

        public Cocktail ShownCocktail
        {
            get { return _shownCocktail; }
            set
            {
                _shownCocktail = value;
                RaisePropertyChanged(() => ShownCocktail);
            }
        }


        #region Constructor
        public CocktailViewModel(CocktailServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
            MessengerInstance.Register<CocktailSearchMessage>(this, RecieveCocktailSearchMessage);
        }
        #endregion


        private void RecieveCocktailSearchMessage(CocktailSearchMessage message)
        {
            IsVisible = true;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    EnterLoading();
                    switch (message.CocktailSearchType)
                    {
                        case CocktailSearchType.Random:
                            {
                                ShowRandomCocktail();
                                break;
                            }
                        case CocktailSearchType.ByIngredients:
                            {

                                break;
                            }
                        case CocktailSearchType.ByName:
                            {

                                break;
                            }

                    }
                }
                finally
                {
                    ExitLoading();
                }
            });
        }

        private void ShowRandomCocktail()
        {
            ShownCocktail = _serviceClient.GetRandomCocktail();
        }
    }
}