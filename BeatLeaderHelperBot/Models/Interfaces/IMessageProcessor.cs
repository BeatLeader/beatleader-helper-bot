namespace BeatLeaderHelperBot.Models {
    internal interface IMessageProcessor {
        Task Process(Discord.IMessage message);
    }
}
