using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Messages;
using BeatLeaderHelperBot.Models;
using Discord;

namespace BeatLeaderHelperBot.Commands {
    internal class HelpKeywordCommand : KeywordMessageCommand {
        protected override bool IgnoreRegistry { get; } = true;
        protected override string[] Keywords => GenericMessagesConfig.Instance.HelpConfig.HelpKeywords;

        protected override async Task ProcessInternal(IMessage message) {
            if (!GenericMessagesConfig.Instance.HelpConfig.UseHelpKeywordListener) return;
            await new HelpOfferMessage().ReplyAsync(message);
        }
    }
}
