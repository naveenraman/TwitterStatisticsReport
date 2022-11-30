using Microsoft.Extensions.Logging;
using TwitterStatisticsReport.Interfaces;

namespace TwitterStatisticsReport
{
    /// <summary>
    /// Logger Service
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        /// <summary>
        /// Logger Service Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        public LoggerService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggerService>();
        }

        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="additionalValues"></param>
        public void LogInformation(string message, params object[]? additionalValues)
        {
            if (additionalValues == null)
                _logger.LogInformation(message);
            else
                _logger.LogInformation(message, additionalValues);
        }

        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="additionalValues"></param>
        public void LogWarning(int eventId, Exception? exception, string? message, params object[]? additionalValues)
        {
            if (additionalValues == null)
                _logger.LogWarning(eventId, exception, message);
            else
                _logger.LogWarning(eventId, exception, message, additionalValues);
        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="additionalValues"></param>
        public void LogError(int eventId, Exception? exception, string? message, params object[]? additionalValues)
        {
            if (additionalValues == null)
                _logger.LogError(eventId, exception, message);
            else
                _logger.LogError(eventId, exception, message, additionalValues);
        }
    }
}