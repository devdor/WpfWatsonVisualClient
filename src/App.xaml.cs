using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfWatsonVisualClient.ViewModels;
using WpfWatsonVisualClient.Views;

namespace WpfWatsonVisualClient {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            var mainWnd = new AppMainView() {
                DataContext = new AppMainViewModel() {
                    WndTitle = Helper.ReadResource<string>("StrMainViewTitle")
                }
            };
            Application.Current.MainWindow = mainWnd;
            mainWnd.Show();
        }
    }
}
