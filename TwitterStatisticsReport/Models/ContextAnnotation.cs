using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class ContextAnnotation
    {
        [JsonProperty("domain")]
        [JsonPropertyName("domain")]
        public Domain Domain { get; set; }

        [JsonProperty("entity")]
        [JsonPropertyName("entity")]
        public Entity Entity { get; set; }
    }
}