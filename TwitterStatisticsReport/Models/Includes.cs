using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class Includes
    {
        [JsonProperty("tweets")]
        [JsonPropertyName("tweets")]
        public List<Tweet> Tweets { get; set; }
    }
}