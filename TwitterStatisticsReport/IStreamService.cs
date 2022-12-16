namespace TwitterStatisticsReport
{
    public interface IStreamService
    {
        Task GetSampleStream();

        Task GetSampleStreamUsingCurl();
    }
}
