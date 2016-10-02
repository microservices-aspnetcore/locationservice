using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace StatlerWaldorfCorp.LocationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
			IConfiguration config = new ConfigurationBuilder()
							.AddCommandLine(args)
							.Build();

			Startup.Args = args;

			var host = new WebHostBuilder()
						.UseKestrel()
						.UseStartup<Startup>()
						.UseConfiguration(config)
						.Build();

			host.Run();
        }
    }
}
