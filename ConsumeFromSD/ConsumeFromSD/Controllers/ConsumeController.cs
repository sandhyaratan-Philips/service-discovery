using System.Threading.Tasks;
using System.Web.Http;

namespace ConsumeFromSD.Controllers
{
    public class ConsumeController : ApiController
    {
        IWeather _weather;
        public ConsumeController()
        {
            _weather = new WeatherService(DiscoveryConfig.DiscoveryClient);
        }
        [HttpGet]
        // GET: Consume
        public async Task<string> Index()
        {
            var str = await _weather.GetData();
            return str;
        }


    }
}
