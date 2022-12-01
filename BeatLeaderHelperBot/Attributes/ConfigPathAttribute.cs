namespace BeatLeaderHelperBot.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class ConfigPathAttribute : Attribute {
        public ConfigPathAttribute(string path) {
            this.path = path;
        }

        public readonly string? path = null;
    }
}
