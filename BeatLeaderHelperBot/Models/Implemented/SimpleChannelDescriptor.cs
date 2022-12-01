using Discord;

namespace BeatLeaderHelperBot.Models {
    internal class SimpleChannelDescriptor : IChannelDescriptor {
        public SimpleChannelDescriptor(string channelName) {
            Name = channelName;
        }
        public SimpleChannelDescriptor(int channelId) {
            Id = channelId;
        }

        public string? Name { get; } = null;
        public int? Id { get; } = null;

        public bool IsValidChannel(IMessageChannel channel) {
            return Name?.Equals(channel.Name) ?? Id?.Equals(channel.Id) ?? false;
        }
    }
}
