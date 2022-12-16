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
        private readonly IStreamService _streamService;

        /// <summary>
        /// Twitter Service Constructor
        /// </summary>
        /// <param name="streamService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TwitterService(IStreamService streamService)
        {
            _streamService = streamService;
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
                await _streamService.GetSampleStreamUsingCurl();
            }
        }
    }
}