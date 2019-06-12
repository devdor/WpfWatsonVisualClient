using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace WpfWatsonVisualClient.ViewModels {
    public class AbstractWndViewModel : BindableBase {
        #region Fields and Properties
        string _wndTitle;

        public string WndTitle {
            get => _wndTitle;
            set => SetProperty(ref _wndTitle, value);
        }

        public bool IsConfirmed {
            get;
            private set;
        }
        #endregion

        #region Commands
        public DelegateCommand<object> OkCommand {
            get; protected set;
        }

        public DelegateCommand<object> CancelCommand {
            get; protected set;
        }
        #endregion

        public AbstractWndViewModel() {

            this.OkCommand = new DelegateCommand<object>(this.RaiseOk);
            this.CancelCommand = new DelegateCommand<object>(this.RaiseCancel);
        }

        public AbstractWndViewModel(string wndTitle)
            : this(){

            this.WndTitle = wndTitle;
        }

        protected void OnAppError(Exception ex) {
            if (ex == null)
                return;

            this.ShowMessageBox(ex.Message, true);
        }

        protected void ShowMessageBox(string msg, bool isError = false) {
            if (String.IsNullOrWhiteSpace(msg))
                return;

            MessageBox.Show(msg,
                ConstValue.ASSEMBLY_NAME, MessageBoxButton.OK, isError ? MessageBoxImage.Error : MessageBoxImage.Information);
        }

        void RaiseOk(object param) {
            this.CloseWindow(param, true);
        }

        void RaiseCancel(object param) {
            this.CloseWindow(param);
        }

        protected void CloseWindow(object obj, bool isConfirmed = false) {

            if (obj == null)
                return;

            this.IsConfirmed = isConfirmed;
            if (obj is Window) {

                ((Window)obj).Close();
            }
        }
    }
}
