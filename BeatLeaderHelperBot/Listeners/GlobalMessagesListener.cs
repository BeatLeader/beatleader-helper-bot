using BeatLeaderHelperBot.Models;
using Discord;

namespace BeatLeaderHelperBot.Listeners {
    internal static class GlobalMessagesListener {
        public static readonly HashSet<IMessageProcessor> listeners = new();

        public static async Task HandleMessageRecieved(IMessage message) {
            if (message.Author.IsBot) return;
            await InvokeListeners(message);
        }

        public static bool AddListener(IMessageProcessor listener) {
            return listeners.Add(listener);
        }
        public static bool RemoveListener(IMessageProcessor reciever) {
            return listeners.Remove(reciever);
        }

        private static async Task InvokeListeners(IMessage message) {
            foreach (var listener in listeners) {
                await listener.Process(message);
            }
        }
    }
}
