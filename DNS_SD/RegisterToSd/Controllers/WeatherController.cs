using System.Web.Http;

namespace RegisterToSd.Controllers
{
    public class WeatherController : ApiController
    {

        [HttpGet]
        public IHttpActionResult SomeActionMethod()
        {
            string asd = "hello";
            return Ok(asd);
        }

        [HttpGet]
        public IHttpActionResult RemoveDNSSD()
        {
            DiscoveryConfig.DiscoveryClient.ShutdownAsync().Wait();
            return Ok();
        }

    }
}
