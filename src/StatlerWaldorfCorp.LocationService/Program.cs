using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace StatlerWaldorfCorp.LocationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
			System.Console.WriteLine("--------------");
			System.Console.WriteLine(System.IO.Path.GetTempPath());
			System.Console.WriteLine("--------------");			
            var config = new ConfigurationBuilder()
								.AddCommandLine(args)
								.Build();

	    	var host = new WebHostBuilder()
							.UseKestrel()
							.UseStartup<Startup>()
							.UseConfiguration(config)
							.Build();

	    	host.Run();
        }
    }
}
