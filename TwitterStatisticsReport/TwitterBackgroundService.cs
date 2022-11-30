using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwitterStatisticsReport.Interfaces;

namespace TwitterStatisticsReport
{
    public sealed class TwitterBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerService _logger;

        public TwitterBackgroundService(
            IServiceProvider serviceProvider,
            ILoggerService logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(TwitterBackgroundService)} is running.");
            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(TwitterBackgroundService)} is working.");
            using IServiceScope scope = _serviceProvider.CreateScope();
            var twitterService =
                scope.ServiceProvider.GetRequiredService<ITwitterService>();
            await twitterService.ProcessTweets(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(TwitterBackgroundService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}