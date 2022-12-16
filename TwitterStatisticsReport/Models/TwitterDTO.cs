using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitterStatisticsReport.Models
{
    public class Data
    {
        [JsonProperty("author_id")]
        [JsonPropertyName("author_id")]
        public string AuthorId { get; set; }

        [JsonProperty("edit_history_tweet_ids")]
        [JsonPropertyName("edit_history_tweet_ids")]
        public List<string> EditHistoryTweetIds { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class TwitterDTO
    {
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }


}
