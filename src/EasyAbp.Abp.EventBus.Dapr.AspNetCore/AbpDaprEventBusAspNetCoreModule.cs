using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EventBus.Dapr.AspNetCore
{
    [DependsOn(
        typeof(AbpDaprEventBusModule),
        typeof(AbpAspNetCoreModule)
        )]
    public class AbpDaprEventBusAspNetCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            services.PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                var options = services.ExecutePreConfiguredActions<DaprServiceBusOptions>();
                mvcBuilder.AddDapr(options?.ConfigreDaprClientBuilder);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(endpointContext =>
                {
                    //endpointContext.ScopeServiceProvider can not using, it will be dispose.
                    endpointContext.ConfigDaprServiceBus(context.Services.GetServiceProviderOrNull());
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseCloudEvents();
        }
    }
}
