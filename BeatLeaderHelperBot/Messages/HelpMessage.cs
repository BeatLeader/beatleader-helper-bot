using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Models;
using Discord;
using Discord.WebSocket;

namespace BeatLeaderHelperBot.Messages {
    internal class HelpMessage : ComponentMessage {
        public HelpMessage() {
            Bot.Client.SelectMenuExecuted += OnSelectMenuExecuted;
        }
        ~HelpMessage() {
            Bot.Client.SelectMenuExecuted -= OnSelectMenuExecuted;
        }

        protected override string Content => GenericMessagesConfig.Instance.MessagesContent.HelpMessageContent;

        protected override HashSet<SelectMenuBuilder> Selectors => new() {
            new("help-problems-menu", Options)
        };

        private static List<SelectMenuOptionBuilder> Options => ProblemsConfig
            .Instance.Options.Select(x => new SelectMenuOptionBuilder(x.Key, x.Value)).ToList();

        private static readonly GenericMessage logMessage = new LogNoticeMessage();
        private static readonly GenericMessage whoCanAskMessage = new WhoCanAskMessage();

        private async Task OnSelectMenuExecuted(SocketMessageComponent component) {
            if (SentMessage?.Id != component.Message.Id || component.Data.CustomId != "help-problems-menu") return;
            StopDeletionTimer();
            await component.Message.DeleteAsync();
            var choise = component.Data.Values.FirstOrDefault() ?? string.Empty;
            if (ProblemsConfig.Instance.TryGetResponse(choise, out var response)) {
                var channel = component.Channel;
                await channel.SendMessageAsync(response);
                await logMessage.SendAsync(channel);
                await whoCanAskMessage.SendAsync(channel);
            }
        }

        protected override Task OnMessageSent() {
            StartDeletionTimer(BotConfig.Instance.MessageDeletionDelay);
            return Task.CompletedTask;
        }
    }
}
