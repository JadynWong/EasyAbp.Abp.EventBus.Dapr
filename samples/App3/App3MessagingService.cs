using Microsoft.Extensions.Logging;
using SharedModule;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace App3
{
    public class App3MessagingService : ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ILogger _logger;

        public App3MessagingService(IDistributedEventBus distributedEventBus, ILoggerFactory loggerFactory)
        {
            _distributedEventBus = distributedEventBus;
            _logger = loggerFactory.CreateLogger<App3MessagingService>();
        }

        public async Task RunAsync(string message)
        {
            if (!message.IsNullOrEmpty())
            {
                _logger.LogInformation($"{message} send to the App1.");
                await _distributedEventBus.PublishAsync(new App3ToApp1TextEventData(message));
            }
            else
            {
                await _distributedEventBus.PublishAsync(new App3ToApp1TextEventData("App3 is exiting. Bye bye...!"));
            }
        }
    }
}
