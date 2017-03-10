using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace StatlerWaldorfCorp.LocationService {
    public class Startup
    {
        public static string[] Args {get; set;} = new string[] {};
        private ILogger logger;
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);                

            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;
            this.loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();

            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {                                    
            //var transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            var transient = true;
            if (Configuration.GetSection("transient") != null) {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }
            if (transient) {
                logger.LogInformation("Using transient location record repository.");
                services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            } else {                
                var connectionString = Configuration.GetSection("postgres:cstr").Value;                        
                services.AddEntityFrameworkNpgsql().AddDbContext<LocationDbContext>(options =>
                    options.UseNpgsql(connectionString));
                logger.LogInformation("Using '{0}' for DB connection string.", connectionString);
                services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            }
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
