namespace BeatLeaderHelperBot.Models {
    internal interface IMessageDescriptor {
        bool IsValidMessage(string message);
    }
}
