using System;
using System.IO;
using System.Reflection;

namespace WpfWatsonVisualClient {
    class ConstValue {

        public static readonly string ASSEMBLY_NAME = Assembly.GetExecutingAssembly().GetName().Name;
        public const string DEFAULT_SERVICE_URL = "https://gateway.watsonplatform.net/visual-recognition/api";
        public static readonly DateTime DEFAULT_VERSION_DATE = new DateTime(2018, 03, 19);
        public static readonly string USER_SETTINGS_PATH = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ASSEMBLY_NAME);
        public static readonly string APP_SETTINGS_NAME = Path.Combine(
            ConstValue.USER_SETTINGS_PATH, "settings.json");

    }
}
