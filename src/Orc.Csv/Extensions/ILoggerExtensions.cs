namespace Orc.Csv;

using System.Diagnostics;
using Microsoft.Extensions.Logging;

internal static class ILoggerExtensions
{
    private static readonly bool IsDebuggerAttached;

    static ILoggerExtensions()
    {
        IsDebuggerAttached = Debugger.IsAttached;
    }

    public static void LogDebugIfAttached(this ILogger logger, string message)
    {
        if (IsDebuggerAttached)
        {
            logger.LogDebug(message);
        }
    }
}
