using Discord;

namespace BeatLeaderHelperBot.Models {
    internal abstract class ComponentMessage : GenericMessage {
        protected override string? Content { get; }
        protected virtual HashSet<ButtonBuilder>? Buttons { get; }
        protected virtual HashSet<SelectMenuBuilder>? Selectors { get; }

        protected override async Task<IUserMessage?> SendAsyncInternal(IMessageChannel channel) {
            return await channel.SendMessageAsync(Content, components: CreateBuilder().Build());
        }
        protected override async Task<IUserMessage?> ReplyAsyncInternal(IMessage message) {
            try {
                return await message.Channel.SendMessageAsync(Content,
                    messageReference: new(message.Id), components: CreateBuilder().Build());
            } catch (Exception ex) {
                Bot.Log("Failed to reply to the message! " + ex.Message);
                return await Task.FromResult<IUserMessage?>(null);
            }
        }

        protected ComponentBuilder CreateBuilder() {
            var builder = new ComponentBuilder();
            if (Buttons != null) {
                foreach (var item in Buttons) {
                    builder.WithButton(item);
                }
            }
            if (Selectors != null) {
                foreach (var item in Selectors) {
                    builder.WithSelectMenu(item);
                }
            }
            return builder;
        }
    }
}
