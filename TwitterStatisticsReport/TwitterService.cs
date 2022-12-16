using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
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
        private readonly ILoggerService _logger;
        private const string SampleStreamUrl = "/2/tweets/sample/stream?tweet.fields=created_at";

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
                /* Some reason the api call is timing out */
                //await GetSampleStream(cancellationToken);
                await GetSampleStreamUsingCurl(cancellationToken);
            }
        }


        /// <summary>
        /// Get Sample Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task GetSampleStream(CancellationToken cancellationToken)
        {
            _httpClient.BaseAddress = new Uri(_apiUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            var response = await _httpClient.GetAsync(SampleStreamUrl, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(Events.FailedRequest, new ApplicationException(response.Content.ToString()),
                    "Failed To Get Twitter Stream");
                return;
            }
            dynamic json = JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync(cancellationToken))!;
            _logger.LogInformation("Successfully retrieved Twitter Stream {0}", json.ToString());
        }

        /// <summary>
        /// Get Sample Stream Using Curl
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task GetSampleStreamUsingCurl(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "curl.exe",
                    Arguments = "\"" + _apiUrl + SampleStreamUrl + "\"" + " -H " + "\"Authorization: Bearer " + _bearerToken + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using Process? process = Process.Start(start);
                using StreamReader reader = process!.StandardOutput;

                // Limiting the call to only pull 10 records
                for (int i = 0; i <= 10; i++)
                {
                    var data = await reader.ReadLineAsync();
                    Console.Write(JsonConvert.DeserializeObject<object>(data!));
                }
                _logger.LogInformation("Successfully retrieved Twitter Stream {0}", 10);
            }
        }
    }
}