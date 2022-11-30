using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class Tweet
    {
        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonProperty("edit_history_tweet_ids")]
        [JsonPropertyName("edit_history_tweet_ids")]
        public List<string> EditHistoryTweetIds { get; set; }

        [JsonProperty("lang")]
        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonProperty("source")]
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        [JsonPropertyName("in_reply_to_user_id")]
        public string InReplyToUserId { get; set; }

        [JsonProperty("entities")]
        [JsonPropertyName("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("author_id")]
        [JsonPropertyName("author_id")]
        public string AuthorId { get; set; }

        [JsonProperty("referenced_tweets")]
        [JsonPropertyName("referenced_tweets")]
        public List<ReferencedTweet> ReferencedTweets { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("public_metrics")]
        [JsonPropertyName("public_metrics")]
        public PublicMetrics PublicMetrics { get; set; }

        [JsonProperty("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("possibly_sensitive")]
        [JsonPropertyName("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }
    }
}