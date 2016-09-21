using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Steeltoe.Extensions.Configuration;
using Steeltoe.CloudFoundry.Connector.PostgreSql.EFCore;
using Microsoft.Extensions.Logging;

namespace StatlerWaldorfCorp.LocationService {
    public class Startup
    {
        private ILogger logger;
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddCloudFoundry();

            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;
            this.loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();

            this.logger = this.loggerFactory.CreateLogger("Startup");            
        }
                
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if(Configuration.GetConnectionString("LocationConnString") != null) {
                logger.LogInformation("PostgreSql: connecting by environment variable specifieid.");

                services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("LocationConnString")));
            } else {
                logger.LogInformation("PostgreSql: connecting to CF via SteelToe.");
                
                services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(Configuration));
            }

            services.AddScoped<ILocationRecordRepository, PostgresLocationRecordRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();            
        }
    }   
}
