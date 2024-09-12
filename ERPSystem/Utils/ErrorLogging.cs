using System;
using System.IO;
using System.Threading.Tasks;

namespace ERPSystem.Logging
{
    public class ErrorLogging
    {
        private readonly string _logFilePath;

        public ErrorLogging(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public async Task LogErrorAsync(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex), "Exception cannot be null.");
            }

            var directory = Path.GetDirectoryName(_logFilePath);
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                throw new InvalidOperationException("The log file path does not contain a directory.");
            }

            try
            {
                await File.AppendAllTextAsync(_logFilePath, $"Error: {ex.Message}{Environment.NewLine}");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("An error occurred while logging the error.", e);
            }

        }
    }
}