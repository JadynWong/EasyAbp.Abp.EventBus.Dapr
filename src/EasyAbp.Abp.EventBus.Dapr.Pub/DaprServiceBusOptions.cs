using System;
using Dapr.Client;

namespace EasyAbp.Abp.EventBus.Dapr
{
    public class DaprServiceBusOptions
    {
        public Action<DaprClientBuilder> ConfigreDaprClientBuilder { get; set; }

        public string PubSubName { get; set; }
    }
}
