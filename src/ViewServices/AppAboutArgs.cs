using System;

namespace WpfWatsonVisualClient.ViewServices {
    class AppAboutArgs : AbstractViewArgs {
        #region Fields and Properties
        public string AppTitle {
            get;
            set;
        }

        public string VersionInfo {
            get;
            set;
        }
        #endregion
        public AppAboutArgs(string wndTitle)
            : base(wndTitle) {
        }
    }
}
