using Steeltoe.Common.Discovery;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumeFromSD
{
    public class WeatherService : IWeather
    {
        DiscoveryHttpClientHandler _handler;

        private const string weather_api = "http://myfocalpoint/api/Weather/SomeActionMethod";

        public WeatherService(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
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
            var client = new HttpClient(_handler, false);
            //client.BaseAddress = new System.Uri("http://localhost:5021/");
            return client;
        }
    }
}