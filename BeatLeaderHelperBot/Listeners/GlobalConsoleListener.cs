using BeatLeaderHelperBot.Models;

namespace BeatLeaderHelperBot.Listeners {
    internal static class GlobalConsoleListener {
        public static readonly HashSet<IConsoleInputProcessor> listeners = new();
        private static CancellationTokenSource? cancellationTokenSource = null;

        public static void StartListening() {
            cancellationTokenSource = new();
            ConsoleListenerTask(cancellationTokenSource.Token);
        }
        public static void StopListening() {
            if (cancellationTokenSource == null) return;
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }

        public static bool AddListener(IConsoleInputProcessor listener) {
            return listeners.Add(listener);
        }
        public static bool RemoveListener(IConsoleInputProcessor reciever) {
            return listeners.Remove(reciever);
        }

        private static async Task ConsoleListenerTask(CancellationToken token) {
            var stream = Console.In;
            while (true) {
                var line = await stream.ReadLineAsync();
                if (string.IsNullOrEmpty(line)) continue;
                await InvokeListeners(line);
            }
        }
        private static async Task InvokeListeners(string text) {
            foreach (var listener in listeners) {
                await listener.Process(text);
            }
        }
    }
}
