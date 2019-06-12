using Prism.Mvvm;
using System.Windows.Media;

namespace WpfWatsonVisualClient.ViewModels {
    public class ListBoxItem {
        #region Fields and Properties
        public string ImgSrc {
            get;
            set;
        }
        public string FileName {
            get;
            set;
        }
        public string Result {
            get;
            set;
        }
        #endregion
    }
}
