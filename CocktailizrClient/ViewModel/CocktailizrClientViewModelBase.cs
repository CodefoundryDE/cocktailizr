using System;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CocktailizrClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CocktailizrClientViewModelBase : ViewModelBase, IDisposable
    {
        #region Properties

        public const string IsVisiblePropertyName = "IsVisible";
        private bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return IsInDesignMode || _isVisible;
            }
            set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public bool IsLoading
        {
            get { return _loadingOperationsCount != 0; }
        }

        #endregion

        #region Variables

        private int _loadingOperationsCount;
        private readonly object _syncRoot = new object();
        private bool _disposed = false;
        private Random _random = new Random();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CocktailizrClientViewModelBase class.
        /// </summary>
        public CocktailizrClientViewModelBase()
        {
            _loadingOperationsCount = 0;
            if (IsInDesignMode)
            {
                IsVisible = true;
            }
        }

        #endregion

        #region Methods

        protected void EnterLoading()
        {
            lock (_syncRoot)
            {
                _loadingOperationsCount += 1;
            }
            RaisePropertyChanged(() => IsLoading);
            Thread.Sleep(((int)_random.Next(5) * 800) );
        }

        protected void ExitLoading()
        {
            lock (_syncRoot)
            {
                if (_loadingOperationsCount > 0)
                {
                    _loadingOperationsCount -= 1;
                }
                else
                {
                    _loadingOperationsCount = 0;
                }
            }
            RaisePropertyChanged(() => IsLoading);
        }

        protected void ResetLoadingState()
        {
            lock (_syncRoot)
            {
                _loadingOperationsCount = 0;
            }
            RaisePropertyChanged(() => IsLoading);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            // Free any managed objects here.
            if (disposing)
            {
                Cleanup();
            }

            // Free any unmanaged objects here.
            _disposed = true;
        }

        ~CocktailizrClientViewModelBase()
        {
            Dispose(false);
        }

        #endregion
    }
}