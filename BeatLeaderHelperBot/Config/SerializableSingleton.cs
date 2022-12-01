using BeatLeaderHelperBot.Attributes;
using BeatLeaderHelperBot.Utils;
using Newtonsoft.Json;

namespace BeatLeaderHelperBot.Config {
    internal abstract class SerializableSingleton<T> {
        private static readonly string ConfigsPath = BotConfig.Instance.ConfigPath;

        public static T Instance {
            get {
                if (_instance != null) return _instance;
                LoadConfig();
                return _instance;
            }
        }
        public static bool IsSingletonAvailable => _instance != null;

        private static T? _instance;

        public static void LoadConfig() {
            _instance = default;
            if (!TryGetContent(out var content)) {
                try {
                    if (!Directory.Exists(ConfigsPath)) {
                        Directory.CreateDirectory(ConfigsPath);
                    }
                    _instance = Activator.CreateInstance<T>();
                    var json = JsonConvert.SerializeObject(_instance, Formatting.Indented);
                    File.WriteAllText(GetContentPath(), json);
                } catch (Exception ex) {
                    Bot.Log($"Failed to save default config({typeof(T).Name})! {ex.Message}");
                }
            } else {
                try {
                    _instance = JsonConvert.DeserializeObject<T>(content);
                } catch (Exception ex) {
                    Bot.Log($"Failed to load config({typeof(T).Name})! {ex.Message}");
                }
            }
            _instance ??= Activator.CreateInstance<T>();
        }
        private static bool TryGetContent(out string content) {
            var path = GetContentPath();
            return !string.IsNullOrEmpty(content = File.Exists(path) ? File.ReadAllText(path) : string.Empty);
        }
        private static string GetContentPath() {
            return (typeof(T).TryGetAttribute<ConfigPathAttribute>(out var attr)
                ? attr.path : ConfigsPath) + $"{typeof(T).Name}.json";
        }
    }
}
