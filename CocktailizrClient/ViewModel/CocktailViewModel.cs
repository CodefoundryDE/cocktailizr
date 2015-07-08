using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using GalaSoft.MvvmLight.CommandWpf;

namespace CocktailizrClient.ViewModel
{
    public class CocktailViewModel : CocktailizrClientViewModelBase
    {
        #region Properties
        private CocktailServiceClient _serviceClient;

        private IList<Cocktail> _searchResults = new List<Cocktail>();

        public IList<Cocktail> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                ShownCocktail = _searchResults.FirstOrDefault();
                RaisePropertyChanged(() => SearchResults);
            }
        }


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
        #endregion

        #region Commands

        public ICommand BackToSearchClickedCommand { get { return new RelayCommand(NavigateBackToSearch); } }

        public ICommand NextCocktailCommand { get { return new RelayCommand(ShowNextCocktail); } }

        public ICommand PreviousCocktailCommand { get { return new RelayCommand(ShowPreviousCocktail); } }

        #endregion

        #region Constructor
        public CocktailViewModel(CocktailServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
            MessengerInstance.Register<CocktailSearchMessage>(this, RecieveCocktailSearchMessage);
        }
        #endregion

        #region Methods
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
                                ShowCocktailWithGivenIngredients(message.Ingredients);
                                break;
                            }
                        case CocktailSearchType.ByName:
                            {
                                ShowSearchResults(message.SearchString);
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
            try
            {
                ShownCocktail = _serviceClient.GetRandomCocktail();
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
        }

        private void ShowCocktailWithGivenIngredients(IEnumerable<Zutat> ingredients)
        {
            Cocktail[] results = _serviceClient.GetCocktailsByIndigrents(ingredients.ToArray());
            SearchResults = results;
        }

        private void ShowSearchResults(string searchText)
        {
            Cocktail[] results = _serviceClient.GetCocktailsByName(searchText);
            SearchResults = results;
        }

        private void ShowNextCocktail()
        {
            int recentIndex = SearchResults.IndexOf(ShownCocktail);
            int nextIndex = recentIndex + 1;
            if (SearchResults.Count > nextIndex)
                ShownCocktail = SearchResults.ElementAt(nextIndex);
        }

        private void ShowPreviousCocktail()
        {
            int recentIndex = SearchResults.IndexOf(ShownCocktail);
            int previousIndex = recentIndex - 1;
            if (previousIndex >= 0)
                ShownCocktail = SearchResults.ElementAt(previousIndex);
        }

        private void NavigateBackToSearch()
        {
            MessengerInstance.Send(new LoadSearchMessage { LoadExtendedSearch = false });
            IsVisible = false;
        }

        #endregion
    }
}