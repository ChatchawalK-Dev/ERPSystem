using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Monitoring
{
    public class LogAnalyzer
    {
        private readonly string _logFilePath;

        public LogAnalyzer(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public async Task AnalyzeLogsAsync()
        {
            if (string.IsNullOrEmpty(_logFilePath) || !File.Exists(_logFilePath))
            {
                throw new FileNotFoundException("The log file was not found.", _logFilePath);
            }

            try
            {
                var logEntries = await ReadLogFileAsync();
                var summary = GenerateSummary(logEntries);

                Console.WriteLine("Log Analysis Summary:");
                foreach (var entry in summary)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to analyze logs: {ex.Message}");
                throw;
            }
        }

        private async Task<List<string>> ReadLogFileAsync()
        {
            var logEntries = new List<string>();

            using (var reader = new StreamReader(_logFilePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    logEntries.Add(line);
                }
            }

            return logEntries;
        }

        private Dictionary<string, int> GenerateSummary(List<string> logEntries)
        {
            var summary = new Dictionary<string, int>();

            foreach (var entry in logEntries)
            {
                
                var logLevel = ParseLogLevel(entry);

                if (logLevel != null)
                {
                    if (summary.ContainsKey(logLevel))
                    {
                        summary[logLevel]++;
                    }
                    else
                    {
                        summary[logLevel] = 1;
                    }
                }
            }

            return summary;
        }

        private string? ParseLogLevel(string logEntry)
        {
            
            var parts = logEntry.Split(' ', 2);
            if (parts.Length > 1)
            {
                var level = parts[0].TrimStart('[').TrimEnd(']');
                return level;
            }

            return null;
        }
    }
}
