namespace BeatLeaderHelperBot.Config {
    internal class ProblemsConfig : SerializableSingleton<ProblemsConfig> {
        public Dictionary<string, string> Options { get; set; } = new();
        public Dictionary<string, string> Responses { get; set; } = new();

        public bool TryGetResponse(string id, out string response) {
            foreach (var pair in Responses) {
                if (id.StartsWith(pair.Key)) {
                    response = pair.Value;
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}
