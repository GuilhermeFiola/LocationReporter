using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MicroserviceCore.LocationReporter.Models;
using MicroserviceCore.LocationReporter.Events;
using MicroserviceCore.LocationReporter.Services;

namespace MicroserviceCore.LocationReporter
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath)
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();

            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));
            services.Configure<TeamServiceOptions>(Configuration.GetSection("teamservice"));

            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(ICommandEventConverter), typeof(CommandEventConverter));
            services.AddSingleton(typeof(ITeamServiceClient), typeof(HttpTeamServiceClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ITeamServiceClient teamServiceClient, IEventEmitter eventEmitter)
        {
            // Asked for instances of singletons during Startup
            // to force initialization early.

            app.UseMvc();
        }
    }
}
