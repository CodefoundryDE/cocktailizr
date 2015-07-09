using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace CocktailizrClient.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DialogViewModel : ViewModelBase, IDialogService, IDisposable
    {
        private const string DefaultConfirmText = "Ok";

        #region Properties

        private Dialog _dialog;
        public Dialog Dialog
        {
            get { return _dialog; }
            set
            {
                _dialog = value;
                RaisePropertyChanged(() => Dialog);
            }
        }

        #endregion

        #region Commands

        public RelayCommand PrimButtonCommand
        {
            get { return new RelayCommand(() => ButtonClick(true)); }
        }

        public RelayCommand SecButtonCommand
        {
            get { return new RelayCommand(() => ButtonClick(false)); }
        }

        #endregion

        #region Variables

        private TaskCompletionSource<bool> _taskCompletionSource;
        private bool _disposed;
        private readonly ConcurrentQueue<Dialog> _dialogQueue;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DialogViewModel class.
        /// </summary>
        public DialogViewModel()
        {
            _dialogQueue = new ConcurrentQueue<Dialog>();
            if (IsInDesignMode)
            {
                Dialog = new Dialog
                {
                    DialogTitle = "Titel",
                    DialogText = "Lorem ipsum dolor sit amet ...",
                    ButtonConfirmText = "PrimButton",
                    ButtonCancelText = "SecButton"
                };
            }
        }

        #endregion

        #region InterfaceMethods

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            var dialog = new Dialog
            {
                DialogTitle = title,
                DialogText = error.Message,
                ButtonConfirmText = buttonText,
                AfterHideCallbackAction = afterHideCallback
            };
            await EnqueueDialog(dialog);
        }

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            var dialog = new Dialog
            {
                DialogTitle = title,
                DialogText = message,
                ButtonConfirmText = buttonText,
                AfterHideCallbackAction = afterHideCallback
            };
            await EnqueueDialog(dialog);
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var dialog = new Dialog
            {
                DialogTitle = title,
                DialogText = message,
                ButtonConfirmText = buttonConfirmText,
                ButtonCancelText = buttonCancelText,
                BoolAfterHideCallbackAction = afterHideCallback
            };
            return await EnqueueDialog(dialog);
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            var dialog = new Dialog
            {
                DialogTitle = title,
                DialogText = message,
                ButtonConfirmText = buttonText,
                AfterHideCallbackAction = afterHideCallback
            };
            await EnqueueDialog(dialog);
        }

        public async Task ShowMessage(string message, string title)
        {
            var dialog = new Dialog
            {
                DialogTitle = title,
                DialogText = message,
                ButtonConfirmText = DefaultConfirmText
            };
            await EnqueueDialog(dialog);
        }

        public async Task ShowMessageBox(string message, string title)
        {
            await ShowMessage(message, title);
        }

        #endregion

        #region Methods

        private async void ButtonClick(bool confirmed)
        {
            if (Dialog.AfterHideCallbackAction != null)
            {
                await Task.Factory.StartNew(() => Dialog.AfterHideCallbackAction());
            }
            else if (Dialog.BoolAfterHideCallbackAction != null)
            {
                await Task.Factory.StartNew(() => Dialog.BoolAfterHideCallbackAction(confirmed));
            }
            _taskCompletionSource.SetResult(confirmed);
            ResetProperties();
            TryExecuteDialogFromQueue();
        }

        private async Task<bool> EnqueueDialog(Dialog dialog)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            dialog.TaskCompletionSource = taskCompletionSource;
            _dialogQueue.Enqueue(dialog);
            if (Dialog == null)
            {
                TryExecuteDialogFromQueue();
            }
            return await taskCompletionSource.Task;
        }

        private void TryExecuteDialogFromQueue()
        {
            if (!_dialogQueue.Any()) return;
            Dialog dialog;
            if (_dialogQueue.TryDequeue(out dialog))
            {
                Dialog = dialog;
                _taskCompletionSource = dialog.TaskCompletionSource;
            }
        }

        private void ResetProperties()
        {
            Dialog = null;
            _taskCompletionSource = null;
        }

        #endregion

        #region Dispose
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Cleanup();
            }
            // Free any unmanaged objects here.
            _disposed = true;
        }

        ~DialogViewModel()
        {
            Dispose(false);
        }
        #endregion
    }

    public sealed class Dialog
    {
        public string DialogTitle { get; set; }
        public string DialogText { get; set; }
        public string ButtonConfirmText { get; set; }
        public string ButtonCancelText { get; set; }
        public Action AfterHideCallbackAction { get; set; }
        public Action<bool> BoolAfterHideCallbackAction { get; set; }
        public TaskCompletionSource<bool> TaskCompletionSource { get; set; }

    }
}