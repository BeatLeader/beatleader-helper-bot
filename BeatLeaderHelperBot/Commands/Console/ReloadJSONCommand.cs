using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Models;

namespace BeatLeaderHelperBot.Commands {
    internal class ReloadJSONCommand : ConsoleCommand {
        protected override string Command { get; } = "reloadcfg";

        protected override Task ProcessInternal(string[] args) {
            Bot.Log("Reloading configs...");
            SerializableSingleton<ProblemsConfig>.LoadConfig();
            SerializableSingleton<GenericMessagesConfig>.LoadConfig();
            Bot.Log("Reload done.");
            return Task.CompletedTask;
        }
    }
}
