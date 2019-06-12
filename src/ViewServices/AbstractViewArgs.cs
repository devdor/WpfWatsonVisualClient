using System;

namespace WpfWatsonVisualClient.ViewServices {
    class AbstractViewArgs {
        #region Fields and Properties
        public string WndTitle {
            get;
            set;
        }
        #endregion

        public AbstractViewArgs(string wndTitle) {
            if (String.IsNullOrWhiteSpace(wndTitle))
                throw new ArgumentNullException("WndTitle");

            this.WndTitle = wndTitle;
        }
    }
}
