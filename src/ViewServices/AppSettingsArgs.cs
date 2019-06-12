using System;

namespace WpfWatsonVisualClient.ViewServices {
    class AppSettingsArgs : AbstractViewArgs {
        #region Fields and Properties
        public IBMSrvSettings SrvSettings {
            get;
            set;
        }
        #endregion
        public AppSettingsArgs(string wndTitle)
            : base(wndTitle) {
        }
    }
}
