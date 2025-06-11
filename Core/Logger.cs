using System;
using System.IO;

namespace UltimateTuner.Core
{
    /// <summary>
    /// Simple logger that writes messages to a log file and the console.
    /// </summary>
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logPath = Path.Combine(AppContext.BaseDirectory, "UltimateTuner.log");

        public static void Log(string message)
        {
            lock (_lock)
            {
                var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                Console.WriteLine(line);
                try
                {
                    File.AppendAllText(_logPath, line + Environment.NewLine);
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}
