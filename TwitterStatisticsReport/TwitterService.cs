using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TwitterStatisticsReport.Interfaces;
using TwitterStatisticsReport.Models;

namespace TwitterStatisticsReport
{
    /// <summary>
    /// Twitter Service
    /// </summary>
    public class TwitterService : ITwitterService
    {
        private readonly string _bearerToken;
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private readonly ILoggerService _logger;
        private int _executionCount = 0;
        private const string SampleStreamUrl = "/2/tweets/sample/stream";

        /// <summary>
        /// Twitter Service Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        /// <param name="httpClient"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TwitterService(ILoggerService logger, IOptions<Settings> settings, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _bearerToken = settings.Value.BearerToken ?? throw new ArgumentNullException();
            _apiUrl = settings.Value.ApiUrl ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Process Tweets
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ProcessTweets(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _executionCount++;

                _logger.LogInformation(
                    "Tweet Processing Service is working. Count: {Count}", _executionCount);

                _httpClient.BaseAddress = new Uri(_apiUrl);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
                var response = await _httpClient.GetAsync(SampleStreamUrl, cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(Events.FailedRequest, new ApplicationException(content),
                        "Failed To Get Twitter Stream");
                    return;
                }
                Console.WriteLine(content);
                _logger.LogInformation("Successfully retrieved Twitter Stream");
                var results = JsonSerializer.Deserialize<List<TwitterStream>>(content, _options);
                if (results == null) return;
                _logger.LogInformation($"Total number of tweets received {results.Count}");
                await Task.Delay(10000, cancellationToken);
            }
        }
    }
}