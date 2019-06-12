using System.Collections.Generic;
using System.Linq;

namespace WpfWatsonVisualClient {
    public static class DictionaryUtils {
        public static T TryGetValue<T, K>(this Dictionary<K, T> self, K key) where T : class {
            if (self.TryGetValue(key, out T ret))
                return ret;

            return null;
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> me) => !me?.Any() ?? true;
    }
}
