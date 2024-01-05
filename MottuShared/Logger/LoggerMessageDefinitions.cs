using Microsoft.Extensions.Logging;

namespace MottuShared.Logger
{
    public static partial class LoggerMessageDefinitions
    {
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Information,
            Message = "{Message}",
            SkipEnabledCheck = true
        )]
        public static partial void LogSimpleMessage(this ILogger logger, string message);

        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Error,
            Message = "{Message}",
            SkipEnabledCheck = true
        )]
        public static partial void LogSimpleErrorMessage(this ILogger logger, string message);      
    }
}
