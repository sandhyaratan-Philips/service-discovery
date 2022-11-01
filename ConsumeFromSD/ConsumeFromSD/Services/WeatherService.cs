using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Eureka.AppInfo;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumeFromSD
{
    public class WeatherService : IWeather
    {
        DiscoveryHttpClientHandler _handler;
        Applications _applications;

        private const string weather_api = "/api/Weather/SomeActionMethod";

        public WeatherService(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _applications = ((Steeltoe.Discovery.Eureka.DiscoveryClient)client).Applications;
        }

        public async Task<string> GetData()
        {
            try
            {
                var client = GetClient();
                var str = await client.GetStringAsync(weather_api);
                return str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        private HttpClient GetClient()
        {
            Application focalPointApp = _applications.GetRegisteredApplication("myfocalpoint");
            InstanceInfo instanceInfo = focalPointApp.GetInstance("scep");
            var builder = new UriBuilder("http", instanceInfo.HostName, instanceInfo.Port);

            var client = new HttpClient(_handler, false)
            {
                BaseAddress = builder.Uri
            };
            return client;
        }
    }
}