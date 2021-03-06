﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using CocktailizrTypes.Security;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;

namespace CocktailizrClient.ViewModel
{
    public class CocktailViewModel : CocktailizrClientViewModelBase
    {
        #region Properties
        private ObservableCollection<Cocktail> _searchResults = new ObservableCollection<Cocktail>();

        public ObservableCollection<Cocktail> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                ShownCocktail = _searchResults.FirstOrDefault();
                if (ShownCocktail != null && ShownCocktail.Rezept != null &&
                    ShownCocktail.Rezept.ZubereitungsSchritte != null)
                {
                    Steps = new ObservableCollection<Step>(ShownCocktail.Rezept.ZubereitungsSchritte);
                    ShownStep = ShownCocktail.Rezept.ZubereitungsSchritte.FirstOrDefault();
                }

                RaisePropertyChanged(() => SearchResults);
                RaisePropertyChanged(() => HasNextCocktail);
                RaisePropertyChanged(() => HasPreviousCocktail);
            }
        }

        public bool HasNextCocktail
        {
            get
            {
                int recentIndex = SearchResults.IndexOf(ShownCocktail);
                int nextIndex = recentIndex + 1;
                return SearchResults.Count > nextIndex;
            }
        }

        public bool HasPreviousCocktail
        {
            get
            {
                int recentIndex = SearchResults.IndexOf(ShownCocktail);
                int previousIndex = recentIndex - 1;
                return previousIndex >= 0;
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
                if (value != null && value.Rezept != null && value.Rezept.ZubereitungsSchritte != null)
                {
                    Steps = new ObservableCollection<Step>(value.Rezept.ZubereitungsSchritte);
                    ShownStep = _steps.FirstOrDefault();
                }
            }
        }

        private ObservableCollection<Step> _steps;

        public ObservableCollection<Step> Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                RaisePropertyChanged(() => Steps);
                RaisePropertyChanged(() => HasNextStep);
                RaisePropertyChanged(() => HasPreviousStep);
            }
        }

        private Step _shownStep;


        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                RaisePropertyChanged(() => IsAdmin);
            }
        }

        public Step ShownStep
        {
            get { return _shownStep; }
            set
            {
                _shownStep = value;
                RaisePropertyChanged(() => ShownStep);
            }
        }

        public bool HasNextStep
        {
            get
            {
                if (ShownStep != null)
                {
                    int recentIndex = Steps.IndexOf(ShownStep);
                    int nextIndex = recentIndex + 1;
                    return Steps.Count > nextIndex;
                }
                return false;
            }
        }

        public bool HasPreviousStep
        {
            get
            {
                if (ShownStep != null)
                {
                    int recentIndex = Steps.IndexOf(ShownStep);
                    int previousIndex = recentIndex - 1;
                    return previousIndex >= 0;
                }
                return false; ;
            }
        }


        #endregion

        #region Variables

        private ViewModelLocator _viewModelLocator;
        private IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand BackToSearchClickedCommand { get { return new RelayCommand(NavigateBackToSearch); } }

        public ICommand NextCocktailCommand { get { return new RelayCommand(ShowNextCocktail); } }

        public ICommand PreviousCocktailCommand { get { return new RelayCommand(ShowPreviousCocktail); } }

        public ICommand NextStepCommand { get { return new RelayCommand(ShowNextStep); } }

        public ICommand PreviousStepCommand { get { return new RelayCommand(ShowPreviousStep); } }

        public ICommand EditCocktailCommand { get { return new RelayCommand(NavigateToCocktailEdit); } }

        #endregion

        #region Validation

        #endregion

        #region Constructor
        public CocktailViewModel(ViewModelLocator viewModelLocator, IDialogService dialogService)
        {
            _viewModelLocator = viewModelLocator;
            _dialogService = dialogService;
            MessengerInstance.Register<CocktailSearchMessage>(this, RecieveCocktailSearchMessage);
            MessengerInstance.Register<LoginMessage>(this, RecieveLoginMessage);
        }
        #endregion

        #region Methods
        private void RecieveCocktailSearchMessage(CocktailSearchMessage message)
        {
            ShownCocktail = null;
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

        private void RecieveLoginMessage(LoginMessage message)
        {
            if (LoginAction.RoleChange != message.LoginAction)
            {
                return;
            }
            IsAdmin = _viewModelLocator.AdminServiceClient.GetUserRole() == UserRole.Admin;
        }

        private void ShowRandomCocktail()
        {
            Cocktail result = _viewModelLocator.CocktailServiceClient.GetRandomCocktail();
            SearchResults = new ObservableCollection<Cocktail> { result };
        }

        private void ShowCocktailWithGivenIngredients(IEnumerable<Zutat> ingredients)
        {
            Cocktail[] results = _viewModelLocator.CocktailServiceClient.GetCocktailsByIndigrents(ingredients.ToArray());
            SearchResults = new ObservableCollection<Cocktail>(results);
            if (!results.Any())
            {
                _dialogService.ShowMessage("Ihre Anfrage liefert keine Ergebnisse", "Achtung", "Ok", NavigateBackToSearch);
            }
        }

        private void ShowSearchResults(string searchText)
        {
            Cocktail[] results = _viewModelLocator.CocktailServiceClient.GetCocktailsByName(searchText);
            SearchResults = new ObservableCollection<Cocktail>(results);
            if (!results.Any())
            {
                _dialogService.ShowMessage("Ihre Anfrage liefert keine Ergebnisse", "Achtung", "Ok", NavigateBackToSearch);
            }
        }

        private void ShowNextCocktail()
        {
            int recentIndex = SearchResults.IndexOf(ShownCocktail);
            int nextIndex = recentIndex + 1;
            ShownCocktail = SearchResults.ElementAt(nextIndex);
            RaisePropertyChanged(() => HasNextCocktail);
            RaisePropertyChanged(() => HasPreviousCocktail);
        }

        private void ShowPreviousCocktail()
        {
            int recentIndex = SearchResults.IndexOf(ShownCocktail);
            int previousIndex = recentIndex - 1;
            ShownCocktail = SearchResults.ElementAt(previousIndex);
            RaisePropertyChanged(() => HasNextCocktail);
            RaisePropertyChanged(() => HasPreviousCocktail);
        }

        private void ShowNextStep()
        {
            int recentIndex = Steps.IndexOf(ShownStep);
            int nextIndex = recentIndex + 1;
            ShownStep = Steps.ElementAt(nextIndex);
            RaisePropertyChanged(() => HasNextStep);
            RaisePropertyChanged(() => HasPreviousStep);
        }

        private void ShowPreviousStep()
        {
            int recentIndex = Steps.IndexOf(ShownStep);
            int previousIndex = recentIndex - 1;
            ShownStep = Steps.ElementAt(previousIndex);
            RaisePropertyChanged(() => HasNextStep);
            RaisePropertyChanged(() => HasPreviousStep);
        }

        private void NavigateBackToSearch()
        {
            MessengerInstance.Send(new LoadSearchMessage { LoadExtendedSearch = false });
            IsVisible = false;
        }

        private void NavigateToCocktailEdit()
        {
            if (ShownCocktail != null)
            {
                MessengerInstance.Send(new LoadAdminMessage { CocktailToBeEdited = ShownCocktail });
                IsVisible = false;
            }
        }

        #endregion
    }
}