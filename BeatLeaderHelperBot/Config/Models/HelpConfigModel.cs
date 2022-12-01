namespace BeatLeaderHelperBot.Config {
    internal class HelpConfigModel {
        public bool UseHelpKeywordListener { get; set; } = true;
        public string[] HelpKeywords { get; set; } = new[] { "help" };
    }
}
