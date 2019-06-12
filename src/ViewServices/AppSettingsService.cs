using System;
using System.ComponentModel;
using System.Windows;
using WpfWatsonVisualClient.ViewModels;
using WpfWatsonVisualClient.Views;

namespace WpfWatsonVisualClient.ViewServices {
    class AppSettingsService {
        #region Delegates
        readonly Action<AppSettingsResultArgs> _callback;
        #endregion

        public AppSettingsService(Action<AppSettingsResultArgs> callback) {
            this._callback = callback;
        }
        public void ShowView(AppSettingsArgs args) {
            if (args == null)
                throw new ArgumentNullException("AppSettingsArgs");

            var win = new AppSettingsView() {

                DataContext = new AppSettingsViewModel(args.WndTitle) {
                    AccessToken = args.SrvSettings?.AccessToken,
                    ServiceUrl = args.SrvSettings?.ServiceUrl,
                    VersionDate = args.SrvSettings?.VersionDate
                }
            };

            win.Closing += this.DialogClosing;
            win.Owner = Application.Current.MainWindow;
            win.ShowDialog();
        }

        private void DialogClosing(object sender, CancelEventArgs e) {
            if (!(sender is AppSettingsView))
                return;

            var args = new AppSettingsResultArgs();
            var wnd = sender as AppSettingsView;
            if (wnd.DataContext != null
                && wnd.DataContext is AppSettingsViewModel) {

                var vm = wnd.DataContext as AppSettingsViewModel;
                args.SrvSettings = vm.SrvSettings;
                args.IsConfirmed = vm.IsConfirmed;
            }

            this._callback?.Invoke(args);
        }
    }
}
