namespace WpfWatsonVisualClient {
    class IBMSrvSettings {
        #region Fields and Properties
        public string ServiceUrl {
            get;
            set;
        }
        public string AccessToken {
            get;
            set;
        }
        public string VersionDate {
            get;
            set;
        }
        #endregion
        public static IBMSrvSettings CreateDefault() {

            return new IBMSrvSettings() {
                ServiceUrl = ConstValue.DEFAULT_SERVICE_URL,
                VersionDate = ConstValue.DEFAULT_VERSION_DATE.ToString("yyyy-MM-dd")
            };
        }
    }
}
