using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TwitterStatisticsReport.Interfaces;
using TwitterStatisticsReport.Models;

namespace TwitterStatisticsReport
{
    public class StreamService : IStreamService
    {
        private readonly string _bearerToken;
        private readonly string _apiUrl;
        private readonly ILoggerService _logger;
        private readonly HttpClient _httpClient;
        private const string SampleStreamUrl = "/2/tweets/sample/stream?tweet.fields=author_id,referenced_tweets";
        private static readonly Dictionary<string, int> _hashTags = new Dictionary<string, int>();
        private static readonly List<TwitterDTO> _twitterEntities = new List<TwitterDTO>();
        private static int _Count = 0;

        public StreamService(IOptions<Settings> settings,HttpClient httpClient, ILoggerService logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _bearerToken = settings.Value.BearerToken ?? throw new ArgumentNullException();
            _apiUrl = settings.Value.ApiUrl ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Get Sample Stream
        /// </summary>
        /// <returns></returns>
        public async Task GetSampleStream()
        {
            _httpClient.BaseAddress = new Uri(_apiUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            var response = await _httpClient.GetAsync(SampleStreamUrl).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(Events.FailedRequest, new ApplicationException(response.Content.ToString()),
                    "Failed To Get Twitter Stream");
                return;
            }

            var twitterContents = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(twitterContents))
            {
                var recentTweets = new List<TwitterDTO>();
                var twitterSplits = twitterContents.Split(new[] { "}}" }, StringSplitOptions.None);
                foreach (var twitterSplit in twitterSplits)
                {
                    if (!string.IsNullOrWhiteSpace(twitterSplit))
                    {
                        var twitterDTO = JsonConvert.DeserializeObject<TwitterDTO>(twitterSplit + "}}");
                        if (!_twitterEntities.Contains(twitterDTO!))
                        {
                            _twitterEntities.Add(twitterDTO!);
                            recentTweets.Add(twitterDTO!);
                        }
                    }
                }
                _Count += _twitterEntities.Count;
                Console.WriteLine("Total number of tweets received : {0}", _Count);
                foreach (var tweet in recentTweets)
                {
                    var tweetText = tweet.Data.Text;
                    if (!string.IsNullOrWhiteSpace(tweetText))
                    {
                        GetHashTagCount(tweetText);
                    }
                }
            }
        }

        /// <summary>
        /// Get Sample Stream Using Curl
        /// </summary>
        /// <returns></returns>
        public async Task GetSampleStreamUsingCurl()
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

                Console.WriteLine("Top 10 HashTags");
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
}
