using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MicroserviceCore.LocationReporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .AddCommandLine(args)
                                        .Build();

            var host = new WebHostBuilder()
                            .UseKestrel()
                            .UseStartup<Startup>()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseConfiguration(config)
                            .Build();

            host.Run();
        }
    }
}
