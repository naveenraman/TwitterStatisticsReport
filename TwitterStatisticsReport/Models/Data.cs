using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class Data
    {
        [JsonProperty("edit_history_tweet_ids")]
        [JsonPropertyName("edit_history_tweet_ids")]
        public List<string>? EditHistoryTweetIds { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}