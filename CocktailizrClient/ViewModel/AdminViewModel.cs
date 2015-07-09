using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrClient.Message;
using CocktailizrTypes.Model.Entities;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;

namespace CocktailizrClient.ViewModel
{
    public class AdminViewModel : CocktailizrClientViewModelBase
    {
        #region Properties

        private ViewModelLocator ViewModelLocator { get; set; }

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

        private ObservableCollection<CocktailZutatProxy> _zutatenCollection;
        public ObservableCollection<CocktailZutatProxy> ZutatenCollection
        {
            get { return _zutatenCollection ?? (_zutatenCollection = new ObservableCollection<CocktailZutatProxy>()); }
            set
            {
                _zutatenCollection = value;
                RaisePropertyChanged(() => ZutatenCollection);
            }
        }

        private ObservableCollection<Step> _zubereitungSteps;
        public ObservableCollection<Step> ZubereitungsSteps
        {
            get { return _zubereitungSteps ?? (_zubereitungSteps = new ObservableCollection<Step>()); }
            set
            {
                _zubereitungSteps = value;
                RaisePropertyChanged(() => ZubereitungsSteps);
            }
        }


        private string _imgFilePath;
        public string ImgFilePath
        {
            get { return _imgFilePath; }
            set
            {
                _imgFilePath = value;
                RaisePropertyChanged(() => ImgFilePath);
                ParseImage();
            }
        }

        #endregion

        #region Commands
        public ICommand SaveClickCommand
        {
            get { return new RelayCommand(SaveRecentCocktail); }
        }

        public ICommand NavigateBackCommand
        {
            get { return new RelayCommand(NavigateBackClick); }
        }




        #endregion

        #region Variables

        private IDialogService _dialogService;

        #endregion

        #region Constructor
        public AdminViewModel(ViewModelLocator viewModelLocator, IDialogService dialogService)
        {
            ViewModelLocator = viewModelLocator;
            _dialogService = dialogService;
            MessengerInstance.Register<LoadAdminMessage>(this, ReceiveLoadAdmin);


        }
        #endregion

        #region Methods
        private void ReceiveLoadAdmin(LoadAdminMessage message)
        {
            IsVisible = true;
            if (message.CocktailToBeEdited == null)
            { return; }

            RecentCocktail = message.CocktailToBeEdited;
            ZutatenCollection = new ObservableCollection<CocktailZutatProxy>();
            foreach (var zutatEntry in RecentCocktail.Zutaten)
            {
                ZutatenCollection.Add(new CocktailZutatProxy(zutatEntry.Key, zutatEntry.Value));
            }
            // ReSharper disable once ExplicitCallerInfoArgument
            ZubereitungsSteps = new ObservableCollection<Step>(RecentCocktail.Rezept.ZubereitungsSchritte);
        }

        private async void SaveRecentCocktail()
        {
            EnterLoading();
            //Zutaten zurückschreiben
            var zutatenDictionary = ZutatenCollection.ToDictionary(cocktailZutatProxy => new Zutat
            {
                Name = cocktailZutatProxy.Name,
                Skala = cocktailZutatProxy.Skala,
                IsOptional = cocktailZutatProxy.IsOptional
            }, cocktailZutatProxy => cocktailZutatProxy.Menge);
            RecentCocktail.Zutaten = zutatenDictionary;

            RecentCocktail.Rezept.ZubereitungsSchritte = ZubereitungsSteps;

            Exception exception = null;
            try
            {
                if (RecentCocktail.Id.Equals(Guid.Empty))
                {
                    await ViewModelLocator.AdminServiceClient.AddCocktailAsync(RecentCocktail);
                }
                else
                {
                    await ViewModelLocator.AdminServiceClient.ModifyCocktailAsync(RecentCocktail.Id, RecentCocktail);
                }
                IsVisible = false;
                MessengerInstance.Send(new LoadSearchMessage { LoadExtendedSearch = false });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                ExitLoading();
            }
            if (exception != null)
            {
                await _dialogService.ShowMessage(exception.Message, "Bei dem Versuch den Datensatz zu speichern kam es zu Fehlern");
            }

        }

        private void NavigateBackClick()
        {
            IsVisible = false;
            MessengerInstance.Send(new LoadSearchMessage { LoadExtendedSearch = false });
        }

        private async void ParseImage()
        {
            EnterLoading();

            await Task.Factory.StartNew(() =>
            {
                Exception exception = null;
                try
                {
                    RecentCocktail.Image = Image.FromFile(ImgFilePath);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                if (exception != null)
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() => _dialogService.ShowMessage("Bitte versuchen sie eine andere Datei einzufügen...",
                        "Fehler beim Einlesen des Bildes"));
                }

            });

            ExitLoading();
        }


        #endregion

    }

    public class CocktailZutatProxy : Zutat
    {
        private decimal _menge;

        public decimal Menge
        {
            get { return _menge; }
            set
            {
                _menge = value;
                OnPropertyChanged();
            }
        }

        public CocktailZutatProxy()
        {

        }

        public CocktailZutatProxy(Zutat zutat, decimal menge)
        {
            Name = zutat.Name;
            Skala = zutat.Skala;
            Menge = menge;
            IsOptional = zutat.IsOptional;
        }



    }

}