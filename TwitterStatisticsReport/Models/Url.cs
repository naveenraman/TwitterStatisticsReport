using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
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
        public string? TUrl { get; set; }

        [JsonProperty("expanded_url")]
        [JsonPropertyName("expanded_url")]
        public string ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        [JsonPropertyName("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("media_key")]
        [JsonPropertyName("media_key")]
        public string MediaKey { get; set; }

        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonProperty("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonProperty("unwound_url")]
        [JsonPropertyName("unwound_url")]
        public string UnwoundUrl { get; set; }
    }
}