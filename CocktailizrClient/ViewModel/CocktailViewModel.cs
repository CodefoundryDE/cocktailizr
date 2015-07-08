using System;
using System.Threading.Tasks;
using CocktailizrClient.Message;

namespace CocktailizrClient.ViewModel
{
    public class CocktailViewModel : CocktailizrClientViewModelBase
    {

        public CocktailViewModel()
        {
            MessengerInstance.Register<CocktailSearchMessage>(this, RecieveCocktailSearchMessage);
        }


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
                                GetRandomCocktail();
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
                IsVisible = true;
            });
        }

        private void GetRandomCocktail()
        {

        }
    }
}