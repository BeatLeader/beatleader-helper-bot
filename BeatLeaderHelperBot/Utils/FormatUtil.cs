using Discord;

namespace BeatLeaderHelperBot.Utils {
    internal static class FormatUtil {
        public static string FormatMessage(this IMessage message) {
            return $"[{message.Timestamp}] ({message.Author}) \"{message.Content}\"";
        }
        public static string FormatMessage(this IMessage message, string command) {
            return FormatMessage(message) + $" caused \"{command}\"";
        }
    }
}
