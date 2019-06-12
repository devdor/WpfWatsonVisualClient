namespace WpfWatsonVisualClient.ViewServices {
    class AppSettingsResultArgs : AbstractViewResultArgs {
        #region Fields and Properties
        public IBMSrvSettings SrvSettings {
            get;
            set;
        }
        #endregion
        public AppSettingsResultArgs() {
        }

        public AppSettingsResultArgs(bool isConfirmed)
            : base(isConfirmed) {
        }
    }
}
