namespace BeatLeaderHelperBot.Config {
    internal class GenericMessagesConfig : SerializableSingleton<GenericMessagesConfig> {
        public MessagesContentModel MessagesContent { get; set; } = new();
        public HelpConfigModel HelpConfig { get; set; } = new();
    }
}
