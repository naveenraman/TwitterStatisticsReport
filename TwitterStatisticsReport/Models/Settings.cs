namespace TwitterStatisticsReport.Models
{
    public class Settings
    {
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
        public string? ApiToken { get; set; }
        public string? ApiTokenSecret { get; set; }

        public string? BearerToken { get; set; }

        public string? ApiUrl { get; set; }
    }
}