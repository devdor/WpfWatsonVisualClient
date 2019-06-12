namespace WpfWatsonVisualClient.ViewModels {
    class AppAboutViewModel : AbstractWndViewModel {
        #region Fields and Properties
        string _versionInfo;
        public string VersionInfo {
            get => _versionInfo;
            set => SetProperty(ref _versionInfo, value);
        }
        
        string _appTitle;
        public string AppTitle {
            get => _appTitle;
            set => SetProperty(ref _appTitle, value);
        }
        #endregion
        public AppAboutViewModel(string wndTitle)
            : base(wndTitle) {
        }
    }
}
