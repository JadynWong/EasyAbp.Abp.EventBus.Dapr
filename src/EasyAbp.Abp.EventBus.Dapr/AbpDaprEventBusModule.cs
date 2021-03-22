using Dapr.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EventBus.Dapr
{
    [DependsOn(typeof(AbpEventBusModule))]
    public class AbpDaprEventBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.Configure<DaprServiceBusOptions>(options =>
            //{
            //    options.PubSubName = "pubsub";
            //});

            var services = context.Services;
            services.AddSingleton(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            });
            // Add Dapr service bus
            services.AddSingleton<IDaprServiceBus, DaprServiceBus>();
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            services.ExecutePreConfiguredActions<DaprServiceBusOptions>();
            services.TryAddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<DaprServiceBusOptions>>();
                var builder = new DaprClientBuilder();
                options.Value.ConfigreDaprClientBuilder?.Invoke(builder);
                return builder.Build();
            });
        }
    }
}
