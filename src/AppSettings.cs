namespace WpfWatsonVisualClient {
    class AppSettings {
        #region Fields and Properties
        public IBMSrvSettings SrvSettings {
            get;
            set;
        }
        #endregion

        public static AppSettings CreateDefault() {

            return new AppSettings() {
                SrvSettings = IBMSrvSettings.CreateDefault()
            };
        }
    }
}
