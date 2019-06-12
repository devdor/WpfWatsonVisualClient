namespace WpfWatsonVisualClient.ViewModels {
    class AppSettingsViewModel : AbstractWndViewModel {
        #region Fields and Properties
        string _serviceUrl;
        public string ServiceUrl {
            get => _serviceUrl;
            set => this.SetProperty(ref _serviceUrl, value);
        }

        string _accessToken;
        public string AccessToken {
            get => _accessToken;
            set => this.SetProperty(ref _accessToken, value);
        }

        string _versionDate;
        public string VersionDate {
            get => _versionDate;
            set => this.SetProperty(ref _versionDate, value);
        }

        public IBMSrvSettings SrvSettings {
            get {
                return new IBMSrvSettings() {
                    AccessToken = this.AccessToken,
                    ServiceUrl = this.ServiceUrl,
                    VersionDate = this.VersionDate
                };
            }
        }
        #endregion
        public AppSettingsViewModel(string wndTitle)
            :base(wndTitle) {
        }
    }
}
