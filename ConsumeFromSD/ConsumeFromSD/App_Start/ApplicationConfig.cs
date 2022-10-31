

using Microsoft.Extensions.Configuration;

namespace ConsumeFromSD
{
    public static class ApplicationConfig
    {
        public static IConfigurationRoot Configuration { get; internal set; }

        public static void RegisterConfig(string environment)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            builder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
    }
}