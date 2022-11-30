using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class PublicMetrics
    {
        [JsonProperty("retweet_count")]
        [JsonPropertyName("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("reply_count")]
        [JsonPropertyName("reply_count")]
        public int ReplyCount { get; set; }

        [JsonProperty("like_count")]
        [JsonPropertyName("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("quote_count")]
        [JsonPropertyName("quote_count")]
        public int QuoteCount { get; set; }
    }
}