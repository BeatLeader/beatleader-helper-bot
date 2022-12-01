using BeatLeaderHelperBot.Messages;
using BeatLeaderHelperBot.Models;
using Discord;

namespace BeatLeaderHelperBot.Commands
{
    internal class HelpCommand : MessageCommand {
        public override string Command { get; } = "help";

        protected override async Task ProcessInternal(IMessage message) {
            await new HelpMessage().SendAsync(message.Channel);
        }
    }
}
