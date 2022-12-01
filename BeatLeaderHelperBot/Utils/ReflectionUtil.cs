using System.Reflection;

namespace BeatLeaderHelperBot.Utils {
    internal static class ReflectionUtil {
        public static T? Cast<T>(this object? o) {
            return o != null ? (T)o : default;
        }

        public static bool TryGetAttribute<T>(this Type type, out T? attribute) where T : Attribute {
            return (attribute = type.GetCustomAttribute<T>()) != null;
        }
    }
}
