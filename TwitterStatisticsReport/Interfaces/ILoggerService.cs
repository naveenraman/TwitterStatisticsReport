namespace TwitterStatisticsReport.Interfaces
{
    /// <summary>
    /// Logger Service Interface
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="additionalValues"></param>
        void LogInformation(string message, params object[]? additionalValues);

        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="additionalValues"></param>
        void LogWarning(int eventID, Exception? exception, string? message, params object[] additionalValues);

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="additionalValues"></param>
        void LogError(int eventId, Exception? exception, string? message, params object[]? additionalValues);
    }
}