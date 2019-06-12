using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;

namespace WpfWatsonVisualClient.ViewModels {
    public class MenuItemViewModel {
        #region Fields and Properties
        public string Header {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> MenuItemList {
            get;
            set;
        }
        #endregion

        #region Commands
        private DelegateCommand _command;
        public DelegateCommand Command {
            get => _command;
            set => _command = value;
        }
        #endregion

        public MenuItemViewModel() {
        }

        public MenuItemViewModel(string header) {
            if (String.IsNullOrWhiteSpace(header))
                throw new ArgumentNullException("Header");

            this.Header = header;
        }
    }
}
