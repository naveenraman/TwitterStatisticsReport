using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class TwitterStream
    {
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("includes")]
        [JsonPropertyName("includes")]
        public Includes Includes { get; set; }
    }
}