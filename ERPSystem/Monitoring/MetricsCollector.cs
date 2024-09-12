using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ERPSystem.Monitoring
{
    public class MetricsCollector
    {
        private readonly Stopwatch _stopwatch;
        private readonly Dictionary<string, int> _metrics;

        public MetricsCollector()
        {
            _stopwatch = new Stopwatch();
            _metrics = new Dictionary<string, int>();
        }

        public void StartCollecting()
        {
            _stopwatch.Start();
        }

        public void StopCollecting(string metricName)
        {
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

            // Store or process the metric
            if (_metrics.ContainsKey(metricName))
            {
                _metrics[metricName] = _metrics[metricName] + 1;
            }
            else
            {
                _metrics[metricName] = 1;
            }

            Console.WriteLine($"{metricName} took {elapsedMilliseconds}ms");
        }

        public void LogMetrics()
        {
            Console.WriteLine("Metrics Report:");
            foreach (var metric in _metrics)
            {
                Console.WriteLine($"{metric.Key}: {metric.Value}");
            }
        }

        public async Task SimulateAsyncWork(string metricName, Func<Task> work)
        {
            StartCollecting();
            await work();
            StopCollecting(metricName);
        }
    }
}
