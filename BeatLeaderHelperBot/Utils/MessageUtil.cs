using Discord.WebSocket;

namespace BeatLeaderHelperBot.Utils {
    internal static class MessageUtil {
        public static async Task<bool> IsClickedByWhoCaused(this SocketMessageComponent component) {
            var author = await component.GetOriginalResponseAsync();
            return component.User != author.Author;
        }
    }
}
