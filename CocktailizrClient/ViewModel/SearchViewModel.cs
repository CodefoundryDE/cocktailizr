using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using GalaSoft.MvvmLight.CommandWpf;

namespace CocktailizrClient.ViewModel
{
    public class SearchViewModel : CocktailizrClientViewModelBase
    {
        #region Properties
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged(() => SearchText);
            }
        }

        #endregion

        #region Commands

        public ICommand RandomClickedCommand
        {
            get { return new RelayCommand(GetRandomCocktail); }
        }

        public ICommand SearchClickedCommand
        {
            get { return new RelayCommand(GetCocktailBySearchString, ValidateSearchString); }
        }

        #endregion

        private void GetRandomCocktail()
        {
            MessengerInstance.Send(new CocktailSearchMessage
            {
                CocktailSearchType = CocktailSearchType.Random
            });
            IsVisible = false;
        }

        private void GetCocktailBySearchString()
        {
            MessengerInstance.Send(new CocktailSearchMessage
            {
                CocktailSearchType = CocktailSearchType.ByName,
                SearchString = SearchText
            });
            IsVisible = false;
        }

        #region Constructor
        public SearchViewModel()
        {
            IsVisible = true;
            MessengerInstance.Register<LoadSucheMessage>(this, LoadSuche);
        }
        #endregion

        private void LoadSuche(LoadSucheMessage obj)
        {
            IsVisible = true;
        }

        private bool ValidateSearchString()
        {
            return !string.IsNullOrEmpty(SearchText);
        }
    }
}
