namespace BeatLeaderHelperBot.Models {
    internal class SimpleMessageDescriptor : IMessageDescriptor {
        public SimpleMessageDescriptor(params string[] keywords) {
            Keywords = keywords;
        }
        public SimpleMessageDescriptor(string message) {
            Message = message;
        }

        public string[]? Keywords { get; } = null;
        public string? Message { get; } = null;

        public bool IsValidMessage(string message) {
            if (message.StartsWith(Bot.Prefix)) return false;
            if (Keywords != null) {
                foreach (var keyword in Keywords) {
                    if (message.Contains(keyword)) return true;
                }
            }
            return Message?.Equals(message) ?? false;
        }
    }
}
