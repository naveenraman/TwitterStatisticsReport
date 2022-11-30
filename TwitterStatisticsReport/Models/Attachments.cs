using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class Attachments
    {
        [JsonProperty("media_keys")]
        [JsonPropertyName("media_keys")]
        public List<string> MediaKeys { get; set; }
    }
}