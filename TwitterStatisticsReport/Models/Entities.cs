using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class Entities
    {
        [JsonProperty("urls")]
        [JsonPropertyName("urls")]
        public List<Url> Urls { get; set; }

        [JsonProperty("annotations")]
        [JsonPropertyName("annotations")]
        public List<Annotation> Annotations { get; set; }
    }
}