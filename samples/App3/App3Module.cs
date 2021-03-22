using EasyAbp.Abp.EventBus.Dapr;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace App3
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpDaprEventBusModule)
    )]
    public class App3Module : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<DaprServiceBusOptions>(options =>
            {
                options.PubSubName = "pubsub";
                options.ConfigreDaprClientBuilder = builder =>
                {
                    //Configure DaprClientBuilder
                };
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<App3HostedService>();
        }
    }
}
