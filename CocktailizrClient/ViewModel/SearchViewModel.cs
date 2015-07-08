using System;
using System.Collections.Generic;
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

        #endregion

        #region Commands

        public ICommand RandomClickedCommand
        {
            get { return new RelayCommand(GetRandomCocktail); }
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

        public SearchViewModel()
        {
            IsVisible = true;
        }
    }
}
