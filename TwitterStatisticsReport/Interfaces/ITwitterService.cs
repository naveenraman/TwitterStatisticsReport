namespace TwitterStatisticsReport.Interfaces
{
    /// <summary>
    /// Twitter Service Interface
    /// </summary>
    public interface ITwitterService
    {
        /// <summary>
        /// Process Tweets
        /// </summary>
        Task ProcessTweets(CancellationToken cancellationToken);
    }
}