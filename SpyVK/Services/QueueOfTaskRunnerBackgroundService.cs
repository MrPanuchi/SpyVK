using SpyVK.Services.Interfaces;
using SpyVK.Services.newVK;

namespace SpyVK.Services
{
    public class QueueOfTaskRunnerBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<QueueOfTaskRunnerBackgroundService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;
        private int requestCount;
        public QueueOfTaskRunnerBackgroundService(
            ILogger<QueueOfTaskRunnerBackgroundService> logger,
            IConfiguration configuration,
            IServiceProvider services)
        {
            _logger = logger;
            _configuration = configuration;
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service - running - Queue of tasks runner.");
            requestCount = int.Parse(_configuration["TaskRunnerConfig:RequestCount"]);

            _timer = new Timer(
                QueueOfTaskRun, 
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromSeconds(int.Parse(_configuration["TaskRunnerConfig:DelayInSeconds"])));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service - stop - Queue of task runner.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void QueueOfTaskRun(object? state)
        {
            using (var scope = _services.CreateScope())
            {
                var queue = scope.ServiceProvider.GetRequiredService<IQueueOfTask>();
                int taskGettingCount = 0;
                while (taskGettingCount <= requestCount)
                {
                    TimeTask task = queue.GetTask();
                    if (task != null)
                    {
                        _logger.LogInformation("Background service - start task - Queue of task runner.");
                        taskGettingCount++;
                        task.StartTask();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
