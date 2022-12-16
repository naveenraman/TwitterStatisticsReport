using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitterStatisticsReport.Models
{
    public class Attachments
    {
        [JsonProperty("media_keys")]
        [JsonPropertyName("media_keys")]
        public List<string> MediaKeys { get; set; }
    }

    public class ContextAnnotation
    {
        [JsonProperty("domain")]
        [JsonPropertyName("domain")]
        public Domain Domain { get; set; }

        [JsonProperty("entity")]
        [JsonPropertyName("entity")]
        public Entity Entity { get; set; }
    }

    public class Data
    {
        [JsonProperty("attachments")]
        [JsonPropertyName("attachments")]
        public Attachments Attachments { get; set; }

        [JsonProperty("author_id")]
        [JsonPropertyName("author_id")]
        public string AuthorId { get; set; }

        [JsonProperty("context_annotations")]
        [JsonPropertyName("context_annotations")]
        public List<ContextAnnotation> ContextAnnotations { get; set; }

        [JsonProperty("conversation_id")]
        [JsonPropertyName("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("edit_controls")]
        [JsonPropertyName("edit_controls")]
        public EditControls EditControls { get; set; }

        [JsonProperty("edit_history_tweet_ids")]
        [JsonPropertyName("edit_history_tweet_ids")]
        public List<string> EditHistoryTweetIds { get; set; }

        [JsonProperty("entities")]
        [JsonPropertyName("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("geo")]
        [JsonPropertyName("geo")]
        public Geo Geo { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("lang")]
        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonProperty("possibly_sensitive")]
        [JsonPropertyName("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty("public_metrics")]
        [JsonPropertyName("public_metrics")]
        public PublicMetrics PublicMetrics { get; set; }

        [JsonProperty("referenced_tweets")]
        [JsonPropertyName("referenced_tweets")]
        public List<ReferencedTweet> ReferencedTweets { get; set; }

        [JsonProperty("reply_settings")]
        [JsonPropertyName("reply_settings")]
        public string ReplySettings { get; set; }

        [JsonProperty("source")]
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Domain
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class EditControls
    {
        [JsonProperty("edits_remaining")]
        [JsonPropertyName("edits_remaining")]
        public int EditsRemaining { get; set; }

        [JsonProperty("is_edit_eligible")]
        [JsonPropertyName("is_edit_eligible")]
        public bool IsEditEligible { get; set; }

        [JsonProperty("editable_until")]
        [JsonPropertyName("editable_until")]
        public DateTime EditableUntil { get; set; }
    }

    public class Entities
    {
        [JsonProperty("urls")]
        [JsonPropertyName("urls")]
        public List<Url> Urls { get; set; }
    }

    public class Entity
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Geo
    {
    }

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

    public class ReferencedTweet
    {
        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class TwitterDTO
    {
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Url
    {
        [JsonProperty("start")]
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonProperty("url")]
        [JsonPropertyName("url")]
        public string TweetUrl { get; set; }

        [JsonProperty("expanded_url")]
        [JsonPropertyName("expanded_url")]
        public string ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        [JsonPropertyName("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("media_key")]
        [JsonPropertyName("media_key")]
        public string MediaKey { get; set; }
    }


}
