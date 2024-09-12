using ERPSystem.Utils;
using Xunit;
using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using ERPSystem.Logging;


namespace ERPSystem.Tests.Utils
{
    public class ErrorLoggingTest
    {
        private readonly string _validLogFilePath = Path.Combine(Path.GetTempPath(), "valid_log.txt");
        private readonly string _invalidLogFilePath = Path.Combine("invalid_directory", "log.txt");

        [Fact]
        public async Task LogErrorAsync_ValidException_LogsErrorSuccessfully()
        {
            // Arrange
            var errorLogging = new ErrorLogging(_validLogFilePath);
            var exception = new Exception("Test exception");

            // Act
            await errorLogging.LogErrorAsync(exception);

            // Assert
            Assert.True(File.Exists(_validLogFilePath), "Log file was not created.");
            var logContent = await File.ReadAllTextAsync(_validLogFilePath);
            Assert.Contains("Error: Test exception", logContent);

            // Cleanup
            File.Delete(_validLogFilePath);
        }
    }
}