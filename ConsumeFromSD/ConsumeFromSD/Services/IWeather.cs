using System.Threading.Tasks;

namespace ConsumeFromSD
{
    public interface IWeather
    {
        Task<string> GetData();
    }
}
