using BeatLeaderHelperBot.Utils;
using Discord;

namespace BeatLeaderHelperBot.Models {
    internal abstract class MessageCommand : IMessageProcessor, IMessageDescriptor, IChannelDescriptor {
        public abstract string Command { get; }

        public async Task Process(IMessage message) {
            if (!IsValidChannel(message.Channel) 
                || !IsValidMessage(message.Content)) return;
            Bot.Log(FormatLog(message));
            await ProcessInternal(message);
        }

        protected abstract Task ProcessInternal(IMessage message);
        protected virtual string FormatLog(IMessage message) {
            return message.FormatMessage(Command);
        }

        public virtual bool IsValidMessage(string message) {
            return message.Equals(Bot.Prefix + Command);
        }
        public virtual bool IsValidChannel(IMessageChannel channel) {
            return true;
        }
    }
}
