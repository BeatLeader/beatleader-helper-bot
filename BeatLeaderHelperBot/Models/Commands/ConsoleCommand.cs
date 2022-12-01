namespace BeatLeaderHelperBot.Models {
    internal abstract class ConsoleCommand : IConsoleInputProcessor {
        protected abstract string Command { get; }

        public async Task Process(string text) {
            var spaceIdx = text.IndexOf(' ');
            bool spaceNotFound = spaceIdx == -1;
            var command = spaceNotFound ? text : text.Remove(spaceIdx);
            if (command != Command) return;
            var argsLine = text.Remove(0, spaceNotFound ? 0 : spaceIdx);
            var args = argsLine.Split(' ');
            await ProcessInternal(args);
        }

        protected abstract Task ProcessInternal(string[] args);
    }
}
