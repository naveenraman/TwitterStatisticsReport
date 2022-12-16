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
        private const string SampleStreamUrl = "/2/tweets/sample/stream?tweet.fields=author_id,referenced_tweets";
        private static readonly Dictionary<string, int> _hashTags = new Dictionary<string, int>();
        private static int _Count = 0;

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
                /* Using curl to get the Twitter Stream */
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
                var start = new ProcessStartInfo
                {
                    FileName = "curl.exe",
                    Arguments = "\"" + _apiUrl + SampleStreamUrl + "\"" + " -H " + "\"Authorization: Bearer " + _bearerToken + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using var process = Process.Start(start);
                using var reader = process!.StandardOutput;

                // Limiting the call to only pull 10 records
                while (!reader.EndOfStream)
                {
                    var tweetDto = JsonConvert.DeserializeObject<TwitterDTO>((await reader.ReadLineAsync())!);
                    if (tweetDto != null)
                    {
                        var text = tweetDto.Data.Text;
                        _Count++;
                        Console.WriteLine("Total number of tweets received : {0}", _Count);
                        if (text != null)
                        {
                            GetHashTagCount(text);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get HashTag Count
        /// </summary>
        /// <param name="text"></param>
        private static void GetHashTagCount(string text)
        {
            var tokens = text.Split(' ');
            if (tokens.Length > 0 && tokens != null)
            {
                foreach (var token in tokens)
                {
                    if (token.StartsWith('#'))
                    {
                        var endIndex = token.IndexOf(' ');
                        if (endIndex == -1)
                            endIndex = token.Length;
                        var hashTag = token.Substring(0, endIndex);
                        if (_hashTags.ContainsKey(hashTag))
                        {
                            _hashTags[hashTag] += 1;
                        }
                        else
                        {
                            _hashTags.Add(hashTag, 1);
                        }
                    }
                }
            }

            Console.WriteLine("Top 10 Hashtags");
            var sortedHashTags = (from entry in _hashTags orderby entry.Value descending select entry)
                .Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var sortedHashTag in sortedHashTags)
            {
                Console.WriteLine("HashTag: {0}, Count: {1}",
                    sortedHashTag.Key, sortedHashTag.Value);
            }
        }
    }
}