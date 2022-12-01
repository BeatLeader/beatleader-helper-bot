using BeatLeaderHelperBot.Attributes;

namespace BeatLeaderHelperBot.Config {
    [ConfigPath("")]
    internal class BotConfig : SerializableSingleton<BotConfig> {
        public string? Token { get; set; }
        public char Prefix { get; set; } = '?';
        public string ConfigPath { get; set; } = "Config\\";
        public int MessageDeletionDelay { get; set; } = 60000;
    }
}
