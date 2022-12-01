using Discord;

namespace BeatLeaderHelperBot.Models {
    internal interface IChannelDescriptor {
        bool IsValidChannel(IMessageChannel channel);
    }
}
