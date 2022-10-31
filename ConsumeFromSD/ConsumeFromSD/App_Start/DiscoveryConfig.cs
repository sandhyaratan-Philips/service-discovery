using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Eureka;
using System;
using System.Linq;

namespace ConsumeFromSD
{
    public static class DiscoveryConfig
    {
        public const string EUREKA_PREFIX = "eureka";

        public static IDiscoveryClient DiscoveryClient { get; internal set; }
        public static void Register(IConfiguration configuration, IDiscoveryLifecycle discoveryLifecycle)
        {
            IServiceInfo info = GetSingletonDiscoveryServiceInfo(configuration);
            DiscoveryClient = CreateDiscoveryClient(info, configuration, discoveryLifecycle);
        }

        private static IServiceInfo GetSingletonDiscoveryServiceInfo(IConfiguration config)
        {
            var eurekaInfos = config.GetServiceInfos<EurekaServiceInfo>();

            if (eurekaInfos.Count > 0)
            {
                if (eurekaInfos.Count != 1)
                {
                    throw new ConnectorException(string.Format("Multiple discovery service types bound to application."));
                }

                return eurekaInfos[0];
            }

            return null;
        }

        private static IDiscoveryClient CreateDiscoveryClient(IServiceInfo info, IConfiguration config, IDiscoveryLifecycle lifecycle)
        {
            var clientConfigsection = config.GetSection(EUREKA_PREFIX);

            int childCount = clientConfigsection.GetChildren().Count();
            EurekaClientOptions clientOptions = new EurekaClientOptions();
            if (childCount > 0)
            {
                EurekaServiceInfo einfo = info as EurekaServiceInfo;
                var clientSection = config.GetSection(EurekaClientOptions.EUREKA_CLIENT_CONFIGURATION_PREFIX);
                ConfigurationBinder.Bind(clientSection, clientOptions);
                if (einfo != null)
                {
                    EurekaPostConfigurer.UpdateConfiguration(config, einfo, clientOptions);
                }


                var instSection = config.GetSection(EurekaInstanceOptions.EUREKA_INSTANCE_CONFIGURATION_PREFIX);
                EurekaInstanceOptions instOptions = new EurekaInstanceOptions();
                ConfigurationBinder.Bind(instSection, instOptions);
                if (einfo != null)
                {
                    EurekaPostConfigurer.UpdateConfiguration(config, einfo, instOptions);
                }
                var manager = new EurekaApplicationInfoManager(new OptionsMonitorWrapper<EurekaInstanceOptions>(instOptions));


                //code to be removed:
                var client = new DiscoveryClient(clientOptions, null);
                do
                {
                    // Get what applications have been fetched
                    var apps = client.Applications;

                    // Try to find app with name "MyApp", it is registered in the Register console application
                    var app = apps.GetRegisteredApplication("MYFOCALPOINT");
                    if (app != null)
                    {
                        // Print the instance info, and then exit loop
                        Console.WriteLine("Successfully fetched application: {0} ", app);
                        break;
                    }

                } while (true);


                //code to be removed:

                // return (IDiscoveryClient)client;
                return new EurekaDiscoveryClient(
                    new OptionsMonitorWrapper<EurekaClientOptions>(clientOptions),
                    new OptionsMonitorWrapper<EurekaInstanceOptions>(instOptions),
                    manager,
                    null);
            }
            else
            {
                throw new ArgumentException("Unable to create Eureka client");
            }
        }

    }
    public class OptionsMonitorWrapper<T> : IOptionsMonitor<T>
    {
        private T Options { get; }
        public OptionsMonitorWrapper(T options)
        {
            Options = options;
        }

        public T CurrentValue => Options;
        public T Get(string name)
        {
            throw new NotImplementedException();
        }

        public IDisposable OnChange(Action<T, string> listener)
        {
            throw new NotImplementedException();
        }
    }
}