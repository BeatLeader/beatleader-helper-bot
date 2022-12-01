using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Models;

namespace BeatLeaderHelperBot.Messages {
    internal class LogNoticeMessage : GenericMessage {
        protected override string Content { get; } = GenericMessagesConfig.Instance.MessagesContent.LogMessageContent;
    }
}
