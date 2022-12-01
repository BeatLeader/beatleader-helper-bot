using Discord;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BeatLeaderHelperBot.Models {
    internal abstract class GenericMessage {
        public GenericMessage() {
            _timer = new Timer();
            _timer.Elapsed += OnTimeElapsed;
        }
        ~GenericMessage() {
            _timer.Elapsed -= OnTimeElapsed;
            _timer.Dispose();
        }

        protected abstract string? Content { get; }

        protected IUserMessage? SentMessage { get; private set; }

        private readonly Timer _timer;

        public async Task<bool> SendAsync(IMessageChannel channel) {
            SentMessage = await SendAsyncInternal(channel);
            await OnMessageSent();
            return SentMessage != null;
        }

        public async Task<bool> ReplyAsync(IMessage message) {
            SentMessage = await ReplyAsyncInternal(message);
            await OnMessageSent();
            return SentMessage != null;
        }

        protected virtual async Task<IUserMessage?> SendAsyncInternal(IMessageChannel channel) {
            return await channel.SendMessageAsync(Content);
        }

        protected virtual async Task<IUserMessage?> ReplyAsyncInternal(IMessage message) {
            try {
                return await message.Channel.SendMessageAsync(Content, messageReference: new(message.Id));
            } catch (Exception ex) { 
                Bot.Log("Failed to reply to the message! " + ex.Message);
                return await Task.FromResult<IUserMessage?>(null);
            }
        }

        protected virtual Task OnMessageSent() { return Task.CompletedTask; }
        protected virtual Task OnMessageDeleted() { return Task.CompletedTask; }

        protected async Task DeleteAsync() {
            if (SentMessage == null) return;
            StopDeletionTimer();
            try {
                await SentMessage.DeleteAsync();
            } catch (Exception ex) {
                Bot.Log("Failed to delete the message! " + ex.Message);
                return;
            }
            SentMessage = null;
            await OnMessageDeleted();
        }
        protected void StartDeletionTimer(int milliseconds) {
            _timer.Interval = milliseconds;
            _timer.Start();
        }
        protected void StopDeletionTimer() {
            _timer.Stop();
        }

        private async void OnTimeElapsed(object? sender, ElapsedEventArgs? e) {
            await DeleteAsync();
        }
    }
}
