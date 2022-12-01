using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Models;
using Discord;
using Discord.WebSocket;

namespace BeatLeaderHelperBot.Messages {
    internal class HelpOfferMessage : ComponentMessage {
        public HelpOfferMessage() {
            Bot.Client.ButtonExecuted += OnButtonExecuted;
        }
        ~HelpOfferMessage() {
            Bot.Client.ButtonExecuted -= OnButtonExecuted;
        }

        protected override string Content => GenericMessagesConfig
            .Instance.MessagesContent.HelpOfferMessageContent + (_pingUser ? " " + _user.Mention : string.Empty);

        private static readonly GenericMessage helpMessage = new HelpMessage();

        protected override HashSet<ButtonBuilder> Buttons { get; } = new() {
            new("Yes", "help-offer-yes-btn"),
            new("Nah, thx", "help-offer-no-btn")
        };

        private bool _pingUser;
        private IUser _user;

        protected override async Task<IUserMessage?> ReplyAsyncInternal(IMessage message) {
            _user = message.Author;
            _pingUser = false;
            IUserMessage? userMessage;
            try {
                userMessage = await base.ReplyAsyncInternal(message);
            } catch {
                Bot.Log($"Missing message({message.Id}) is ignored, mentioning({message.Author}) instead");
                _pingUser = true;
                userMessage = await base.ReplyAsyncInternal(message);
            }
            StartDeletionTimer(BotConfig.Instance.MessageDeletionDelay);
            return userMessage;
        }

        private async Task OnButtonExecuted(SocketMessageComponent component) {
            if (SentMessage?.Id != component.Message.Id || component.User != _user) return;
            switch (component.Data.CustomId) {
                case "help-offer-yes-btn":
                    await helpMessage.SendAsync(SentMessage.Channel);
                    break;
                case "help-offer-no-btn":
                    break;
                default:
                    return;
            }
            StopDeletionTimer();
            await component.Message.DeleteAsync();
        }
    }
}
