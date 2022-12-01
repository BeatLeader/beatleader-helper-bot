using BeatLeaderHelperBot.Utils;
using Discord;

namespace BeatLeaderHelperBot.Models {
    internal abstract class KeywordMessageCommand : MessageCommand {
        public sealed override string Command { get; } = string.Empty;
        protected virtual bool IgnoreRegistry { get; }
        protected abstract string[] Keywords { get; }

        public override bool IsValidMessage(string message) {
            if (message.StartsWith(Bot.Prefix)) return false;
            message = IgnoreRegistry ? message.ToLower() : message;
            foreach (var keyword in Keywords) {
                if (message.Contains(keyword)) return true;
            }
            return false;
        }

        protected override string FormatLog(IMessage message) {
            return message.FormatMessage(GetType().Name);
        }
    }
}
