using System;
using System.Windows;

namespace WpfWatsonVisualClient {
    class Helper {

        public static string Punctuation {
            get {
                return ReadResource<string>("StrPunctuation");
            }
        }

        public static string ReadStringRes(string key) {

            return ReadResource<string>(key);
        }

        public static string ReadStringRes(string key1, string key2) {

            return $"{ReadResource<string>(key1)}{ReadResource<string>(key2)}";
        }

        public static T ReadResource<T>(string key) {
            if (String.IsNullOrEmpty(key))
                return default(T);

            if (Application.Current.Resources.MergedDictionaries != null
                && Application.Current.Resources.MergedDictionaries.Count > 0) {
                foreach (ResourceDictionary rd in Application.Current.Resources.MergedDictionaries) {
                    object resValue = rd[key];
                    if (resValue != null) {

                        return (T)resValue;
                    }
                }
            }

            return default(T);
        }
    }
}
