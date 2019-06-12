using System;
using System.Windows;
using WpfWatsonVisualClient.ViewModels;
using WpfWatsonVisualClient.Views;

namespace WpfWatsonVisualClient.ViewServices {
    class AppAboutViewService {
        public void ShowView(AppAboutArgs args) {
            if (args == null)
                throw new ArgumentNullException("AppAboutArgs");

            var win = new AppAboutView() {

                DataContext = new AppAboutViewModel(args.WndTitle) {
                    VersionInfo = args.VersionInfo,
                    AppTitle = args.AppTitle
                }
            };

            win.Owner = Application.Current.MainWindow;
            win.ShowDialog();
        }
    }
}
