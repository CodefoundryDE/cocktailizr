using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using GalaSoft.MvvmLight.CommandWpf;

namespace CocktailizrClient.ViewModel
{
    public class ExtendedSearchViewModel : CocktailizrClientViewModelBase
    {
        #region Properties
        private ObservableCollection<Zutat> _selectedIngredients = new ObservableCollection<Zutat>();

        public ObservableCollection<Zutat> SelectedIngredients
        {
            get { return _selectedIngredients; }
            set
            {
                _selectedIngredients = value;
                RaisePropertyChanged(() => SelectedIngredients);
            }
        }

        #endregion

        #region Commands
        public ICommand SearchClickedCommand
        {
            get { return new RelayCommand(GetCocktailByIngredients, ValidateAnyCheckboxIsChecked); }
        }

        public ICommand BackToSearchClickedCommand
        {
            get { return new RelayCommand(NavigateBackToSearch); }
        }

        #endregion

        #region Validation
        private bool ValidateAnyCheckboxIsChecked()
        {
            return SelectedIngredients.Any(zutat => zutat.IsSelected);
        }

        #endregion

        #region Constructor

        public ExtendedSearchViewModel(CocktailServiceClient serviceClient)
        {
            MessengerInstance.Register<LoadSearchMessage>(this, RecieveSearchMessage);

            SelectedIngredients = new ObservableCollection<Zutat>(serviceClient.GetAllZutaten());
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

        private void GetCocktailByIngredients()
        {
            var checkedIngredients = SelectedIngredients.Where(zutat => zutat.IsSelected);
            MessengerInstance.Send(new CocktailSearchMessage() { CocktailSearchType = CocktailSearchType.ByIngredients, Ingredients = checkedIngredients });
            IsVisible = false;
        }

        private void NavigateBackToSearch()
        {
            MessengerInstance.Send(new LoadSearchMessage { LoadExtendedSearch = false });
            IsVisible = false;
        }
        #endregion
    }
}