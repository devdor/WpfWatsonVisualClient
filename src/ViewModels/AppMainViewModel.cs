using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.VisualRecognition.v3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using WpfWatsonVisualClient.ViewServices;

namespace WpfWatsonVisualClient.ViewModels {
    public class AppMainViewModel : AbstractWndViewModel {
        #region Fields and Properties
        ObservableCollection<MenuItemViewModel> _menuItemList;
        public ObservableCollection<MenuItemViewModel> MenuItemList {
            get => _menuItemList;
            set => SetProperty(ref _menuItemList, value);
        }

        ObservableCollection<ListBoxItem> _itemList;
        public ObservableCollection<ListBoxItem> ItemList {
            get => _itemList;
            set => SetProperty(ref _itemList, value);
        }

        string _appStatusMsg = Helper.ReadStringRes("StrSrvNotReady");
        public string AppStatusMsg {
            get => _appStatusMsg;
            set => SetProperty(ref _appStatusMsg, value);
        }

        VisualRecognitionService _vSrv = null;
        #endregion

        #region Commands
        public DelegateCommand AppExitCommand {
            get;
            set;
        }

        public DelegateCommand AppNewCommand {
            get;
            set;
        }

        public DelegateCommand AppClearResultListCommand {
            get;
            set;
        }
        #endregion

        public AppMainViewModel() {

            this.AppExitCommand = new DelegateCommand(this.RaiseAppExit);
            this.AppNewCommand = new DelegateCommand(this.RaiseAppNew);
            this.AppClearResultListCommand = new DelegateCommand(this.RaiseClearResultList);

            this.MenuItemList = new ObservableCollection<MenuItemViewModel>() {
                new MenuItemViewModel(Helper.ReadStringRes("StrFile")) {
                    MenuItemList = new ObservableCollection<MenuItemViewModel>() {
                        new MenuItemViewModel(Helper.ReadStringRes("StrSettings", "StrPunctuation")) {
                            Command = new DelegateCommand(this.RaiseAppSettings)
                        },
                        new MenuItemViewModel(Helper.ReadStringRes("StrNew", "StrPunctuation")) {
                            Command = AppNewCommand
                        },
                        new MenuItemViewModel(Helper.ReadStringRes("StrExit")) {
                            Command = AppExitCommand
                        }
                    }
                },
                new MenuItemViewModel(Helper.ReadStringRes("StrHelp")){
                    MenuItemList = new ObservableCollection<MenuItemViewModel>() {
                        new MenuItemViewModel($"{Helper.ReadStringRes("StrAbout")} {Helper.ReadStringRes("StrMainViewTitle", "StrPunctuation")}"){
                            Command = new DelegateCommand(this.RaiseAppAbout)
                        }
                    }
                }
            };
        }

        void RaiseAppAbout() {
            try {
                new AppAboutViewService().ShowView(
                    new AppAboutArgs(
                        $"{Helper.ReadStringRes("StrAbout")} {Helper.ReadStringRes("StrMainViewTitle")}") {
                        AppTitle = ConstValue.ASSEMBLY_NAME,
                        VersionInfo = $"Version {Assembly.GetExecutingAssembly().GetName().Version.ToString()}"
                    });
            }
            catch(Exception ex) {
                this.OnAppError(ex);
            }
        }

        void RaiseClearResultList() {
            try {
                if(this.ItemList != null) {
                    this.ItemList = new ObservableCollection<ListBoxItem>();
                }                
            }
            catch (Exception ex) {
                this.OnAppError(ex);
            }
        }

        bool ValidateJSON(string s) {
            try {
                JToken.Parse(s);
                return true;
            }
            catch (JsonReaderException ex) {

                this.OnAppError(ex);
                return false;
            }
        }

        AppSettings ReadCurrentAppSettings() {

            var tmpData = File.ReadAllText(ConstValue.APP_SETTINGS_NAME);
            if (!String.IsNullOrEmpty(tmpData)
                && this.ValidateJSON(tmpData)) {

                return JsonConvert.DeserializeObject<AppSettings>(tmpData,
                    new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.All
                    });
            }

            return null;
        }

        void RaiseAppSettings() {
            try {

                AppSettings appSettings = null;
                if (!File.Exists(ConstValue.APP_SETTINGS_NAME)) {

                    appSettings = AppSettings.CreateDefault();
                    
                }
                else {

                    appSettings = this.ReadCurrentAppSettings();
                }

                if(appSettings == null) {

                    this.ShowMessageBox(
                        Helper.ReadStringRes("StrAppSettingsWarn"));

                    return;
                }

                new AppSettingsService(r => {
                    if (r.IsConfirmed) {

                        appSettings.SrvSettings = r.SrvSettings;

                        if (!Directory.Exists(ConstValue.USER_SETTINGS_PATH))
                            Directory.CreateDirectory(ConstValue.USER_SETTINGS_PATH);                                               

                        File.WriteAllText(ConstValue.APP_SETTINGS_NAME,
                            JsonConvert.SerializeObject(appSettings,
                            Formatting.Indented, new JsonSerializerSettings {
                                TypeNameHandling = TypeNameHandling.All
                            }));

                        this.InitService(r.SrvSettings);
                    }
                }).ShowView(
                    new AppSettingsArgs(Helper.ReadStringRes("StrSettings")) {
                        SrvSettings = appSettings.SrvSettings
                    });
            }
            catch (Exception ex) {
                this.OnAppError(ex);
            }
        }

        void RaiseAppExit() {
            try {

                Application.Current.Shutdown();
            }
            catch (Exception ex) {
                this.OnAppError(ex);
            }
        }

        void InitService(IBMSrvSettings args) {

            if (args == null)
                throw new ArgumentNullException("IBMSrvSettings");

            this._vSrv = new VisualRecognitionService(
                new TokenOptions() {
                    ServiceUrl = args.ServiceUrl,
                    IamApiKey = args.AccessToken,
                }, args.VersionDate);

            this.AppStatusMsg = Helper.ReadStringRes("StrSrvReady");
        }

        void RaiseAppNew() {
            try {

                AppSettings appSettings = null;
                if (File.Exists(ConstValue.APP_SETTINGS_NAME)) {

                    appSettings = this.ReadCurrentAppSettings();
                }

                if (appSettings == null
                    || appSettings.SrvSettings == null) {

                    this.ShowMessageBox(
                        Helper.ReadStringRes("StrAppSettingsWarn"));

                    return;
                }

                var dlg = new Microsoft.Win32.OpenFileDialog {
                    AddExtension = true,
                    Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
                };

                if (dlg.ShowDialog().GetValueOrDefault()) {

                    if (this._vSrv == null) {
                        this.InitService(appSettings.SrvSettings);
                    }

                    using (MemoryStream ms = new MemoryStream()) {

                        using (FileStream file = new FileStream(
                            dlg.FileName, FileMode.Open, FileAccess.Read)) {

                            file.CopyTo(ms);
                        }

                        var lbItem = new ListBoxItem() {
                            ImgSrc = dlg.FileName,
                            FileName = dlg.SafeFileName
                        };

                        var response = this._vSrv.Classify(ms, dlg.SafeFileName);
                        if(response != null
                            && response.Result != null
                            && !DictionaryUtils.IsNullOrEmpty(response.Result.Images)) {

                            var img = response.Result.Images.FirstOrDefault();
                            if(img != null
                                && !DictionaryUtils.IsNullOrEmpty(img.Classifiers)) {

                                var defaultClassifier = img.Classifiers.FirstOrDefault();
                                if(defaultClassifier != null
                                    && !DictionaryUtils.IsNullOrEmpty(defaultClassifier.Classes)) {

                                    var sb = new StringBuilder();
                                    foreach (var classItem in defaultClassifier.Classes.OrderBy(obj => obj.Score)) {
                                        if (classItem == null)
                                            continue;

                                        sb.AppendLine($"{classItem.ClassName} = {classItem.Score}");
                                    }

                                    lbItem.Result = sb.ToString();
                                }
                            }
                        }

                        if (this.ItemList == null)
                            this.ItemList = new ObservableCollection<ListBoxItem>();

                        this.ItemList.Insert(0, lbItem);
                        this.AppStatusMsg = $"{dlg.SafeFileName} classified";
                    }
                }
            }
            catch (Exception ex) {
                this.OnAppError(ex);
            }
        }
    }
}
