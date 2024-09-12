using System;
using System.Threading;
using System.Threading.Tasks;

namespace ERPSystem.Scheduling
{
    public class ScheduledTaskService : BackgroundService
    {
        public readonly ILogger<ScheduledTaskService> _logger;

        public ScheduledTaskService(ILogger<ScheduledTaskService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running scheduled task at:{time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(24), stoppingToken);
            }
        }
    }
}