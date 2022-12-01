using BeatLeaderHelperBot.Config;
using BeatLeaderHelperBot.Listeners;
using BeatLeaderHelperBot.Models;
using Discord;
using Discord.WebSocket;
using System.Reflection;

internal static class Bot {
    public static readonly string? Token = BotConfig.Instance.Token;
    public static readonly char Prefix = BotConfig.Instance.Prefix;

    public static async Task Main(string[] args) {
        LoadInternal();
        Client.Log += HandleLogMessageRecieved;
        await Client.LoginAsync(TokenType.Bot, Token);
        await Client.StartAsync();
        GlobalConsoleListener.StartListening();
        await Task.Delay(-1);
    }

    public static DiscordSocketClient Client { get; private set; } = new();

    public static void Log(string line) {
        Console.WriteLine($"[{DateTime.UtcNow}] {line}");
    }

    private static void LoadInternal() {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        LoadMessageListeners(types);
        LoadConsoleListeners(types);
        Client.MessageReceived += GlobalMessagesListener.HandleMessageRecieved;
    }

    private static void LoadMessageListeners(Type[] types) {
        foreach (var type in types) {
            if (type.GetInterface(nameof(IMessageProcessor)) == null || type.IsAbstract) continue;
            GlobalMessagesListener.AddListener((IMessageProcessor)Activator.CreateInstance(type));
        }
    }

    private static void LoadConsoleListeners(Type[] types) {
        foreach (var type in types) {
            if (type.GetInterface(nameof(IConsoleInputProcessor)) == null || type.IsAbstract) continue;
             GlobalConsoleListener.AddListener((IConsoleInputProcessor)Activator.CreateInstance(type));
        }
    }

    private static Task HandleLogMessageRecieved(LogMessage message) {
        Console.WriteLine(message.Message);
        return Task.CompletedTask;
    }
}