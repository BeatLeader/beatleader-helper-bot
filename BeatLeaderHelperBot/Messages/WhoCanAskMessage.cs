using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Models;
using Discord;
using Discord.WebSocket;

namespace BeatLeaderHelperBot.Messages {
    internal class WhoCanAskMessage : ComponentMessage {
        public WhoCanAskMessage() {
            Bot.Client.ButtonExecuted += OnButtonExecuted;
        }
        ~WhoCanAskMessage() {
            Bot.Client.ButtonExecuted -= OnButtonExecuted;
        }

        private const ulong HelperId = 574944221161717780;

        protected override string Content => GenericMessagesConfig.Instance.MessagesContent.WhoCanAskMessageContent;

        protected override HashSet<ButtonBuilder>? Buttons { get; } = new() {
            new("Ping!", "ask-offer-ping-btn")
        };

        private async Task OnButtonExecuted(SocketMessageComponent component) {
            if (component.Data.CustomId != "ask-offer-ping-btn") return;
            await component.Message.DeleteAsync();
            await component.Channel.SendMessageAsync($"**(Ping used by {component.User})** {MentionUtils.MentionUser(HelperId)}");
            //await component.Message.ModifyAsync(x => {
            //    x.Components = default(MessageComponent);
            //    x.Content = $" **(Ping used by {component.User})** {MentionUtils.MentionUser(HelperId)}";
            //});
        }
    }
}
