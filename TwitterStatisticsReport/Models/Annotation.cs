using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TwitterStatisticsReport.Models
{
    public class Annotation
    {
        [JsonProperty("start")]
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonProperty("probability")]
        [JsonPropertyName("probability")]
        public double Probability { get; set; }

        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonProperty("normalized_text")]
        [JsonPropertyName("normalized_text")]
        public string NormalizedText { get; set; }
    }
}